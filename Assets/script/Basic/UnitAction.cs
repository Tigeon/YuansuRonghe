using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitAction : ScriptableObject
{
    public enum ActionType{ Attack = 0 , Defend = 1 , Buff = 2, DeBuff = 3, Speical = 4, Sleep = 5};

    [Header("BasicInfo")]
    [TextArea(7, 7)] [SerializeField]public  string ActionDescription;
    [SerializeField] public ActionType actionType;
    [SerializeField] public float WaitSeconds;
    [SerializeField] public Sprite Icon;

    public int GetActionType(){ return (int)actionType; }

    public abstract void OnAction();
}
