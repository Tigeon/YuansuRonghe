using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Buffs/Weak", fileName = "Weak")]

public class Weak : Buff
{
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
        AddStacks(-1);
        if (Stacks <= 0)
        {
            unit.RemoveBuff(this);
        }
    }

    public override void OnAttack(BattleUnit unit)
    {
        unit.ExtraBonus -= 30;    
    }

    public override void OnAttacked(BattleUnit unit)
    {
    }

    public override void OnDamageTaken(BattleUnit unit)
    {
    }
}
