using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UnitData")]

public class UnitData : ScriptableObject
{
    [Header("BasicInfo")]
    [SerializeField] int id;
    [SerializeField] string unitName = "";
    [SerializeField] int maxHp;
    [TextArea(7,7)] [SerializeField] string info = "";    
    [SerializeField] string ImageName = "";
    [SerializeField] List<UnitAction> actionList = new List<UnitAction>();

    public int GetId(){ return id; }
    public string GetName(){ return unitName;}
    public int GetMaxHp(){ return maxHp;}
    public string GetInfo(){ return info; }
    public string GetImageName(){ return ImageName; }
    public List<UnitAction> GetActionList(){ return actionList;}
}
