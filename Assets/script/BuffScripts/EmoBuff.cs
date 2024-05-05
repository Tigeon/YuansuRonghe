using UnityEngine;

[CreateAssetMenu(menuName = "Buffs/EmoBuff", fileName = "EmoBuff")]
public class EmoBuff : Buff
{
    public override void OnApply(BattleUnit unit)
    {
    }

    public override void OnRemove(BattleUnit unit)
    {
        // 可选：移除时的逻辑，例如清除特效
    }

    public override void OnOwnerDeath(BattleUnit unit)
    {
        // 拥有者死亡时的逻辑，通常不需要特别处理
    }

    public override void OnTurnStart(BattleUnit unit)
    {
        // 回合开始时不需要处理
    }

    public override void OnTurnEnd(BattleUnit unit)
    {
        // 如果单位还没有集中Buff，为其添加一个
        var focus = Instantiate(BuffManager.Instance.GetBuff<Focus>()); // 从BuffManager获取Focus并实例化
        if (focus != null)
        {
            unit.AddBuff(focus); // 添加新的集中Buff
        }

    }

    public override void OnAttack(BattleUnit unit)
    {
        // 集中增益不影响攻击
    }

    public override void OnAttacked(BattleUnit unit)
    {
        // 集中增益不影响被攻击
    }

    public override void OnDamageTaken(BattleUnit unit)
    {
        // 集中增益不影响受到的伤害
    }

    public override void AddStacks(int additionalStacks)
    {
        // 集中增益不需要堆叠
    }
}
