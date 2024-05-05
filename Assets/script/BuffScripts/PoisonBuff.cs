using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Buffs/PoisonBuff", fileName = "New PoisonBuff")]

public class PoisonBuff : Buff
{
    public override void OnApply(BattleUnit unit)
    {
        // 可选：当中毒效果首次应用时执行的逻辑，例如播放特效
        owner = unit;
    }

    public override void OnRemove(BattleUnit unit)
    {
        // 可选：当中毒效果被移除时执行的逻辑，例如清除特效
    }

    public override void OnOwnerDeath(BattleUnit unit)
    {
        // 中毒状态下单位死亡时的逻辑，通常不需要特别处理
    }

    public override void OnTurnStart(BattleUnit unit)
    {
        if (Stacks > 0)
        {
            unit.TakeDamage(Stacks); // 根据层数造成伤害
            AddStacks(-1) ; // 每回合开始后层数减一
            if (Stacks <= 0)
            {
                unit.RemoveBuff(this); // 如果层数为零，则移除中毒效果
            }
        }
    }

    public override void AddStacks(int additionalStacks)
    {
        Stacks += additionalStacks; // 增加中毒层数
        owner.UpdateBuffIcon();
    }

    public override void OnTurnEnd(BattleUnit unit)
    {
        // 中毒效果在回合结束时不需要特别处理
    }

    public override void OnAttack(BattleUnit unit)
    {
        // 中毒状态不影响攻击
    }

    public override void OnAttacked(BattleUnit unit)
    {
        // 中毒状态不影响被攻击
    }

    public override void OnDamageTaken(BattleUnit unit)
    {
        // 中毒状态不影响受到伤害
    }
}
