using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Buffs/BleedBuff", fileName = "New BleedBuff")]

public class BleedBuff : Buff
{
    public BleedBuff(int initialStacks)
    {
        Stacks = initialStacks;
    }

    public override void OnApply(BattleUnit unit)
    {
        // 可以在这里做一些初始化，比如根据初始化堆栈数来应用效果
        owner = unit;
    }

    public override void OnRemove(BattleUnit unit)
    {
        // 清理工作，例如移除UI显示或者其他
    }

    public override void OnOwnerDeath(BattleUnit unit)
    {
        // 如果单位死亡，可以在这里处理相关逻辑
    }
    public override void OnTurnStart(BattleUnit unit){

    }
    public override void OnAttack(BattleUnit unit){

    }
    public override void OnAttacked(BattleUnit unit){

    }

    // 调用这个方法来处理受到伤害时的流血效果
    public override void OnDamageTaken(BattleUnit unit)
    {
        // 附加与流血层数相同的伤害
        unit.TakeDamage(Stacks);
    }

    // 每回合结束时调用这个方法来减半流血层数,最少减少1层
    public override void OnTurnEnd(BattleUnit unit)
    {
        AddStacks(-Mathf.Max(1, Stacks / 2));
        if (Stacks <= 0)
        {
            unit.RemoveBuff(this);
        }
    }

    public override void AddStacks(int additionalStacks)
    {
        Stacks += additionalStacks;
    }
}
