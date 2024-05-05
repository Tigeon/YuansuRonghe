using System.Collections.Generic;
using UnityEngine;

public enum NodeType 
{
    Battle,
    EliteBattle,
    Event,
    Shop,
    Rest,
    Boss,
}

public class Node : MonoBehaviour
{
    public NodeType Type { get; private set; }
    public bool IsBossNode { get; set; }
    public NodeUI NodeUI { get; private set; }
    public int BattleID { get; set; }

    public void SetUp(NodeType type) 
    {
        Type = type;
        NodeUI = GetComponent<NodeUI>();
        NodeUI.node = this;
        switch(Type){
            case NodeType.Battle:
                NodeUI.Sprite = NodeGenerator.Instance.battleNodeSprite;
                BattleID = Random.Range(0, BattleControler.Instance.NormalBattleConfigs.Count);
                break;
            case NodeType.EliteBattle:
                NodeUI.Sprite = NodeGenerator.Instance.eliteBattleNodeSprite;
                BattleID = Random.Range(0, BattleControler.Instance.EliteBattleConfigs.Count);
                break;
            case NodeType.Event:
                NodeUI.Sprite = NodeGenerator.Instance.eventNodeSprite;
                break;
            case NodeType.Shop:
                NodeUI.Sprite = NodeGenerator.Instance.shopNodeSprite;
                break;
            case NodeType.Rest:
                NodeUI.Sprite = NodeGenerator.Instance.restNodeSprite;
                break;
            case NodeType.Boss:
                NodeUI.Sprite = NodeGenerator.Instance.bossNodeSprite;
                BattleID = Random.Range(0, BattleControler.Instance.EliteBattleConfigs.Count);

                break;
            default:
                break;
        }

        NodeUI.SetUp();
    }
    
    public void ChangeNodeType(NodeType NodeType){
        Type = NodeType;
        switch(Type){
            case NodeType.Battle:
                NodeUI.Sprite = NodeGenerator.Instance.battleNodeSprite;
                break;
            case NodeType.EliteBattle:
                NodeUI.Sprite = NodeGenerator.Instance.eliteBattleNodeSprite;
                break;
            case NodeType.Event:
                NodeUI.Sprite = NodeGenerator.Instance.eventNodeSprite;
                break;
            case NodeType.Shop:
                NodeUI.Sprite = NodeGenerator.Instance.shopNodeSprite;
                break;
            case NodeType.Rest:
                NodeUI.Sprite = NodeGenerator.Instance.restNodeSprite;
                break;
            case NodeType.Boss:
                NodeUI.Sprite = NodeGenerator.Instance.bossNodeSprite;
                break;
            default:
                break;
        }
        NodeUI.UpdateImage();
    }
}
