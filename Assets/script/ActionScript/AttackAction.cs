using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UnitActions/AttackAction")]
public class AttackAction : UnitAction
{
    [SerializeField] int Value;
    public override void OnAction()
    {
        BattleControler.Instance.ActingUnit.Attack(BattleControler.Player, Value);
    }

}
