using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

public enum NodeState
{
    Current,
    Reachable,
    None,
    NotReachable
}

public class NodeUI : MonoBehaviour
{
    public Node node{ get; set;}
    public List<NodeUI> ConnectedNodes;// { get; private set; }
    public int ConnectionCount = 0;
    public Sprite Sprite;
    public int Layer;

    public void SetUp()
    {
        ConnectedNodes = new List<NodeUI>();
        ConnectionCount = 0;
        UpdateImage();
    }

    public void UpdateImage()
    {
        this.GetComponent<Image>().sprite = Sprite;
    }

    public void ConnectTo(NodeUI otherNode) 
    {
        ConnectedNodes.Add(otherNode);
        otherNode.ConnectionCount++;
    }

    public void SetState(NodeState state)
    {
        switch (state)
        {
            case NodeState.Current:
                ChangeNodeColor(Color.green);
                break;
            case NodeState.Reachable:
                ChangeNodeColor(Color.yellow);
                break;
            case NodeState.None:
                ChangeNodeColor(Color.white);
                break;
            case NodeState.NotReachable:
                ChangeNodeColor(Color.gray);
                break;
            default:
                break;
        }
    }

    public void ChangeNodeColor(Color color){
        this.GetComponent<Image>().color = color;
    }

    public void MoveToThis()
    {
        if(NodeMove.Instance.IsMoving){
            NodeMove.Instance.MoveToNode(this);
        }
    }
}
