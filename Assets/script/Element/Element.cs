using System;
using System.Collections.Generic;
using UnityEngine;

public enum ElementType
{
    Fire = 0,
    Water = 1,
    Earth = 2,
    Air = 3,
    Wood = 4
}

[CreateAssetMenu(fileName = "New Element", menuName = "Game/Element")]
public class Element : ScriptableObject
{
    [SerializeField] public ElementType elementType;
    [SerializeField] private string elementName;
    [SerializeField] private string description;
    [SerializeField] private Sprite icon;

    // 反应映射字典，映射到无参数的 Action
    private static Dictionary<(ElementType, ElementType), Action> reactions = new Dictionary<(ElementType, ElementType), Action>();

    void OnEnable()
    {
        InitializeReactions();
    }

    // 初始化反应映射
    private void InitializeReactions()
    {
        // 示例反应：火与水反应产生蒸汽
        reactions[(ElementType.Fire, ElementType.Water)] = () => 
        {
            Debug.Log("Steam created! This cools down the area.");
        };

        // 可以在这里定义更多的反应
    }

    public string Name => elementName;
    public string Description => description;
    public Sprite Icon => icon;

    public override string ToString()
    {
        return elementName;
    }

    // 检查两个元素是否可以反应
    public static bool CanReact(Element e1, Element e2)
    {
        return reactions.ContainsKey((e1.elementType, e2.elementType)) || reactions.ContainsKey((e2.elementType, e1.elementType));
    }

    // 触发反应的方法
    public static void React(ElementType type1, ElementType type2)
    {
        if (reactions.TryGetValue((type1, type2), out var reaction))
        {
            reaction();
        }
        else if (reactions.TryGetValue((type2, type1), out reaction)) // 检查反向组合
        {
            reaction();
        }
        else
        {
            Debug.Log("No reaction for this combination.");
        }
    }
}
