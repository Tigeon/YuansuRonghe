using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataBase : MonoBehaviour
{
    [Header("Spells Data Base")]
    [SerializeField] private List<Element> Elements = new List<Element>();
    [SerializeField] private List<Spells> SpellsList = new List<Spells>();
    public static List<Spells> staticSpellsList { get; private set; }
    public static List<UnitData> staticUnitDataList { get; private set; }

    [Header("Unit Data Base")]
    [SerializeField] private List<UnitData> unitDataList = new List<UnitData>();

    public static List<Buff> staticBuffList { get; private set; }

    [Header("Buff Data Base")]
    [SerializeField] private List<Buff> buffList = new List<Buff>();
    public int commonSpellWeight = 1;
    public int rareSpellWeight = 2;
    public int epicSpellWeight = 3;
    public int legendarySpellWeight = 4;

    public static List<Spells> FireSpells = new List<Spells>();
    public static List<Spells> WaterSpells = new List<Spells>();
    public static DataBase Instance { get; private set; }


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        staticUnitDataList = unitDataList;
        staticBuffList = buffList;

        staticSpellsList = SpellsList;
    }

    public Element GetElement(ElementType ElementType)
    {
        return Elements.FirstOrDefault(x => x.elementType == ElementType);
    }

    public static Spells FindSpell(List<Element> inputElements)
    {
        foreach (Spells spell in staticSpellsList)
        {
            List<Element> requiredElements = spell.RequiredElement;

            // 检查输入的元素数量是否与法术需要的元素数量相同
            if (inputElements.Count != requiredElements.Count)
            {
                continue;  // 如果数量不匹配，则跳过当前法术
            }

            // 逐个比较每个位置的元素是否相同
            bool allMatch = true;
            for (int i = 0; i < inputElements.Count; i++)
            {
                if (inputElements[i].elementType != requiredElements[i].elementType)
                {
                    allMatch = false;
                    break;  // 如果任何一个元素不匹配，则停止检查当前法术
                }
            }

            // 如果所有元素完全匹配，则返回当前法术
            if (allMatch)
            {
                return spell;
            }
        }

        // 如果没有找到匹配的法术，则返回null
        return null;
    }

}
