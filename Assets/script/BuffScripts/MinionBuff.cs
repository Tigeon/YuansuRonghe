using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Buffs/MinionBuff", fileName = "New MinionBuff")]
public class MinionBuff : Buff
{
    private BattleUnit master;

    public override void OnApply(BattleUnit unit)
    {
        // 仆从应用时不需要做什么特别处理
        this.master = unit;
    }

    public override void OnRemove(BattleUnit unit)
    {
        // 仆从移除时不需要做什么特别处理
    }

    public override void OnOwnerDeath(BattleUnit unit)
    {
        if (unit == master)
        {
            unit.Dead();  // 如果主人死亡，则该仆从也死亡
        }
    }
    public override void OnTurnStart(BattleUnit unit){
    }
    public override void OnTurnEnd(BattleUnit unit){
    }
    public override void OnAttack(BattleUnit unit){
    }
    public override void OnAttacked(BattleUnit unit){
    }
    public override void OnDamageTaken(BattleUnit unit)
    {
    }
    public override void AddStacks(int additionalStacks)
    {
    }

}