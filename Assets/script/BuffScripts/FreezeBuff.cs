using UnityEngine;



[System.Serializable]
[CreateAssetMenu(menuName = "Buffs/FreezeBuff", fileName = "New FreezeBuff")]
public class FreezeBuff : Buff
{
    public override void OnApply(BattleUnit unit)
    {
        unit.isFrozen = true;
    }

    public override void OnRemove(BattleUnit unit)
    {
        unit.isFrozen = false;
    }

    public override void OnOwnerDeath(BattleUnit unit)
    {
        // 如果单位死亡时有特殊逻辑可以在这里处理
    }

    public override void OnTurnStart(BattleUnit unit)
    {

    }

    public override void OnTurnEnd(BattleUnit unit)
    {
        unit.RemoveBuff(this);
        Debug.Log("冰冻状态结束");
    }

    public override void OnAttack(BattleUnit unit)
    {
        // 冰冻状态下可能不允许攻击
    }

    public override void OnAttacked(BattleUnit unit)
    {
        // 被攻击时可能有特殊效果
    }

    public override void OnDamageTaken(BattleUnit unit)
    {
        // 受到伤害时可能有特殊效果
    }

    public override void AddStacks(int additionalStacks)
    {
        // 冰冻状态不需要层数
    }
}
