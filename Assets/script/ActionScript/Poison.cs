using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UnitActions/Poison")]
public class Poison : UnitAction
{

    public override void OnAction()
    {
        ApplyPoison(BattleControler.Player, 3);
    }
    public void ApplyPoison(BattleUnit target, int initialStacks)
    {
        var poison = BuffManager.Instance.GetBuff<PoisonBuff>();
        poison.Stacks = initialStacks;
        target.AddBuff(poison);
        poison.OnApply(target);
        Debug.Log("Poison applied to " + target.name);
    }

}
