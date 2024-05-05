using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeGenerator : MonoBehaviour
{
    public Node nodePrefab;
    public int nodesPerLayer = 5;
    public int numberOfLayers = 3;
    public float lineWidth = 0.5f;
    public GameObject linePrefab;
    public GameObject NodeParent;
    public GameObject MapUI;
    public float randomness = 0.1f; // percentage of random displacement

    public Sprite battleNodeSprite;
    public Sprite eliteBattleNodeSprite;
    public Sprite eventNodeSprite;
    public Sprite shopNodeSprite;
    public Sprite restNodeSprite;
    public Sprite bossNodeSprite;

    public static List<List<Node>> staticLayers { get; private set; }

    private RectTransform rectTransform;

    public static NodeGenerator Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        rectTransform = NodeParent.GetComponent<RectTransform>();
    }

    private void Start()
    {
        GenerateMap();
        HideMap();
    }

    public void GenerateMap()
    {
        staticLayers = GenerateNodes();
        ConnectNodes(staticLayers);
    }

    public void ShowHideMap(){
        if(MapUI.gameObject.activeSelf){
            HideMap();
        }else{
            ShowMap();
        }
    }

    public void ShowMap(){
        MapUI.gameObject.SetActive(true);
    }
    
    public void HideMap(){
        MapUI.gameObject.SetActive(false);
    }

    private List<List<Node>> GenerateNodes()
    {
        // Calculate spacing based on parent size
        Vector2 spacing = new Vector2(rectTransform.rect.width / nodesPerLayer, rectTransform.rect.height / numberOfLayers);

        // Create a list of node layers
        List<List<Node>> layers = new List<List<Node>>();

        // Create all layers first
        for (int layerIndex = numberOfLayers - 1; layerIndex >= 0; layerIndex--)
        {
            List<Node> layer = new List<Node>();

            // Determine the number of nodes for this layer. If it's the first or last layer, it's 1. Otherwise, it's nodesPerLayer.
            int numberOfNodesInThisLayer = (layerIndex == 0 || layerIndex == numberOfLayers - 1) ? 1 : nodesPerLayer;

            for (int i = 0; i < numberOfNodesInThisLayer; i++)
            {
                Node Node = CreateNode(i, layerIndex, spacing);
                layer.Add(Node);
            }

            // Randomly remove 1 or 2 nodes from the layer
            // Only for non-first and non-last layers
            if (layerIndex != 0 && layerIndex != numberOfLayers - 1)
            {
                int nodesToRemove = Random.Range(1, 3);
                for (int i = 0; i < nodesToRemove; i++)
                {
                    int indexToRemove = Random.Range(0, layer.Count);
                    Destroy(layer[indexToRemove].gameObject);
                    layer.RemoveAt(indexToRemove);
                }
            }

            layers.Insert(0, layer); // Insert each layer at the start of the list to reverse the order
        }

        
        // Randomly set node types
        bool hasShop = false; // 表示是否已经创建了一个商店
        int eliteBattles = 0; // 表示已经创建的精英战斗节点数
        int restAfterElite = 0; // 表示在精英战斗节点之后创建的休息节点数

        for (int layerIndex = 0; layerIndex < numberOfLayers; layerIndex++)
        {
            for (int i = 0; i < layers[layerIndex].Count; i++)
            {
                Node Node = layers[layerIndex][i];

                // 最后一层为Boss节点
                if (layerIndex == numberOfLayers - 1) 
                {
                    Node.ChangeNodeType(NodeType.Boss);
                    continue;
                }

                // boss房下方是休息格
                if (layerIndex == numberOfLayers - 2)
                {
                    Node.ChangeNodeType(NodeType.Rest);
                    continue;
                }

                float randomValue = Random.value;
                if (randomValue < 0.3)
                {
                    // 30%几率生成事件格
                    Node.ChangeNodeType(NodeType.Event);
                }
                else if (!hasShop)
                {
                    // 创建一个商店节点
                    Node.ChangeNodeType(NodeType.Shop);
                    hasShop = true;
                }
                else if (eliteBattles < 3 && randomValue < 0.5)
                {
                    // 随机1-2个战斗格会变为精英战斗
                    Node.ChangeNodeType(NodeType.EliteBattle);
                    eliteBattles++;

                    // 50%的几率在精英战斗的上方或者下方生成一个休息格
                    if (Random.value < 0.4)
                    {
                        restAfterElite = layerIndex + (Random.value < 0.5 ? -1 : 1);
                    }
                }
                else if (layerIndex == restAfterElite)
                {
                    // 在精英战斗的上方或者下方生成一个休息格
                    Node.ChangeNodeType(NodeType.Rest);
                }
                else
                {
                    // 其他情况为战斗格
                    Node.ChangeNodeType(NodeType.Battle);
                }
            }
        }

        System.Random random = new System.Random();

        // Connect the nodes after all nodes have been created
        for (int layerIndex = numberOfLayers - 2; layerIndex >= 0; layerIndex--)
        {
            for (int i = 0; i < layers[layerIndex].Count; i++)
            {
                Node Node = layers[layerIndex][i];

                // Always connect to the nearest node in the next layer
                if (layers[layerIndex + 1].Count > 0)
                {
                    Node.NodeUI.ConnectTo(layers[layerIndex + 1][Mathf.Min(i, layers[layerIndex + 1].Count - 1)].NodeUI);
                }

                // 30% chance to connect to the second nearest node, if it is within 3 nodes away
                if (random.NextDouble() < 0.3 && i < layers[layerIndex + 1].Count - 1 && i >= 1)
                {
                    Node.NodeUI.ConnectTo(layers[layerIndex + 1][i - 1].NodeUI);
                }
            }
        }

        // After all layers are created, ensure that every node is connected to by at least one node
        for (int layerIndex = 1; layerIndex < numberOfLayers; layerIndex++)
        {
            for (int i = 0; i < layers[layerIndex].Count; i++)
            {
                Node Node = layers[layerIndex][i];
                if (Node.NodeUI.ConnectionCount == 0)
                {
                    layers[layerIndex - 1][Mathf.Min(i, layers[layerIndex - 1].Count - 1)].NodeUI.ConnectTo(Node.NodeUI);
                }
            }
        }

        NodeMove.Instance.SetUp(layers[0][0].NodeUI);

        return layers;
    }


    private Node CreateNode(int index, int layerIndex, Vector2 spacing)
    {
        // Create a node
        Node Node = Instantiate(nodePrefab, NodeParent.transform);

        Node.SetUp(NodeType.Battle);

        Node.NodeUI.Layer = layerIndex;

        // Set node's position
        int nodesInThisLayer = (layerIndex == 0 || layerIndex == numberOfLayers - 1) ? 1 : nodesPerLayer;
        float xPos = (nodesInThisLayer == 1) ? 0.5f : (index + 0.5f) * spacing.x - rectTransform.rect.width / 2;
        float yPos = (layerIndex + 0.5f) * spacing.y - rectTransform.rect.height / 2;
        Vector3 position = new Vector3(xPos, yPos, 0);

        // Add a bit of randomness to the position
        float randomX = Random.Range(-spacing.x, spacing.x) * randomness;
        float randomY = Random.Range(-spacing.y, spacing.y) * randomness;
        position += new Vector3(randomX, randomY, 0);

        Node.transform.localPosition = position;

        return Node;
    }

    // Call NodeParent after generating the nodes
    public void ConnectNodes(List<List<Node>> layers)
    {
        foreach (var layer in layers)
        {
            foreach (var node in layer)
            {
                foreach (var connectedNode in node.NodeUI.ConnectedNodes)
                {
                    ConnectNodes(node, connectedNode.node);
                }
            }
        }
    }

    private void ConnectNodes(Node a, Node b)
    {
    // Instantiate an Image prefab for the line
    GameObject lineObj = Instantiate(linePrefab, NodeParent.transform);
    Image lineImage = lineObj.GetComponent<Image>();
    RectTransform lineRectTransform = lineObj.GetComponent<RectTransform>();

    // Calculate the direction vector from node A to node B
    Vector3 direction = b.transform.position - a.transform.position;

    // Calculate the angle to rotate the line
    float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

    // Set the line's position to the midpoint between the nodes
    lineRectTransform.position = a.transform.position + direction / 2;

    // Set the length of the line to match the distance between nodes
    lineRectTransform.sizeDelta = new Vector2(lineWidth, direction.magnitude); // Ensure line width is set here

    // Rotate the line to align with the direction vector
    lineRectTransform.rotation = Quaternion.Euler(0, 0, -angle);

    // Set proper pivot to align it correctly
    lineRectTransform.pivot = new Vector2(0.5f, 0.5f);  // Center pivot to rotate properly

    // Optionally, move the line to be under the nodes in the UI hierarchy
    lineRectTransform.SetAsFirstSibling(); // Ensures that lines do not overlap nodes visually
    }

}
