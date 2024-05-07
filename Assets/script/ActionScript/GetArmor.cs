using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UnitActions/GetArmor")]
public class GetArmor : UnitAction
{
    [SerializeField] int Value;
    public override void OnAction()
    {
        BattleControler.Instance.ActingUnit.GetArmor(Value);
    }

}
