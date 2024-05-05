using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NodeMove : MonoBehaviour
{
    private NodeUI currentNode;
    private int currentLayer = 0;
    private List<NodeUI> reachableNodes = new List<NodeUI>();

    public static NodeMove Instance { get; private set; }

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
    }

    public bool IsMoving { get; private set; }

    public void SetUp(NodeUI startNode)
    {
        currentNode = startNode;
        currentNode.SetState(NodeState.Current);
        reachableNodes = currentNode.ConnectedNodes;
        foreach (var node in reachableNodes)
        {
            node.SetState(NodeState.Reachable);
        }
    }

    public void MoveToNode(NodeUI targetNode)
    {
        // Check if the target node is reachable
        if (reachableNodes.Contains(targetNode))
        {
            // Update the state of the current node
            if (currentNode != null)
            {
                currentNode.SetState(NodeState.Reachable);
            }

            // Move to the target node
            currentNode = targetNode;
            currentNode.SetState(NodeState.Current);
            currentLayer = currentNode.Layer;

            // Update the reachable nodes
            reachableNodes = currentNode.ConnectedNodes;
            foreach (var node in reachableNodes)
            {
                node.SetState(NodeState.Reachable);
            }
            UpdateNodeStates();

            switch (currentNode.node.Type)
            {
                case NodeType.Battle:
                    BattleControler.Instance.StartNormalBattle(currentNode.node.BattleID);
                    CanvasManager.Instance.ShowBattleCanvas();
                    break;
                case NodeType.EliteBattle:
                    BattleControler.Instance.StartEliteBattle(currentNode.node.BattleID);
                    CanvasManager.Instance.ShowBattleCanvas();
                    break;
                case NodeType.Event:
                    EventManager.Instance.TriggerRandomEvent();
                    CanvasManager.Instance.ShowEventCanvas();
                    break;
                case NodeType.Shop:
                    // ShopManager.Instance.StartShop();
                    break;
                case NodeType.Rest:
                    // RestManager.Instance.StartRest();
                    break;
                case NodeType.Boss:
                    BattleControler.Instance.StartBossBattle(currentNode.node.BattleID);
                    CanvasManager.Instance.ShowBattleCanvas();
                    break;
                default:
                    break;
            }
            StopMoving();
            NodeGenerator.Instance.HideMap();
        }
        else
        {
            Debug.LogError("The target node is not reachable!");
        }
    }

    public void UpdateNodeStates()
    {
        var nodes = NodeGenerator.staticLayers;
        for (int i = 0; i < nodes.Count; i++)
        {
            foreach (var node in nodes[i])
            {
                if (i < currentLayer || (i == currentLayer && node.NodeUI != currentNode))
                {
                    node.NodeUI.SetState(NodeState.NotReachable);
                }
            }
        }
    }

    public void StartMoving()
    {
        IsMoving = true;
        NodeGenerator.Instance.ShowMap();
    }

    public void StopMoving()
    {
        IsMoving = false;
        NodeGenerator.Instance.HideMap();
    }
}
