using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UnitActions/ApplyBuff")]
public class ApplyBuff : UnitAction
{
    [SerializeField]List<int> buffStacks;
    [SerializeField]List<Buff> buff;
    [SerializeField] bool TargetPlayer = false;
    public override void OnAction()
    {
        if (TargetPlayer)
        {
            ApplyBuffToPlayer();
        }
        else
        {
            ApplyBuffToEnemy();
        }
    }
    public void ApplyBuffToPlayer()
    {
        for (int i = 0; i < buff.Count; i++)
        {
            buff[i].Stacks = buffStacks[i];
            BattleControler.Player.AddBuff(buff[i]);
        }
    }

    public void ApplyBuffToEnemy()
    {
        for (int i = 0; i < buff.Count; i++)
        {
            buff[i].Stacks = buffStacks[i];
            BattleControler.Instance.ActingUnit.AddBuff(buff[i]);
        }
    }

}
