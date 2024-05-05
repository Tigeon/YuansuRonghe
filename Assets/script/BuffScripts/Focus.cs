using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Buffs/Focus", fileName = "New Focus")]

public class Focus : Buff
{
    public int damagePerStack = 1;
    
    public override void OnApply(BattleUnit unit)
    {
        owner = unit;
    }

    public override void OnRemove(BattleUnit unit)
    {
    }

    public override void OnOwnerDeath(BattleUnit unit)
    {
    }

    public override void OnTurnStart(BattleUnit unit)
    {
    }

    public override void AddStacks(int additionalStacks)
    {
        Stacks += additionalStacks;
    }

    public override void OnTurnEnd(BattleUnit unit)
    {
    }

    public override void OnAttack(BattleUnit unit)
    {
        unit.ExtraDamage += Stacks * damagePerStack;
    }

    public override void OnAttacked(BattleUnit unit)
    {
    }

    public override void OnDamageTaken(BattleUnit unit)
    {
    }
}
