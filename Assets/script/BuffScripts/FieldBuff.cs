using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Buffs/FieldBuff", fileName = "New FieldBuff")]

public class FieldBuff : Buff
{
    int remainingTurns = 2;

    public override void OnApply(BattleUnit unit)
    {
        owner = unit;
        //无效化其他GlobeBuff
        foreach (Buff buff in unit.BuffList)
        {
            if (buff.GetBuffType() == buffType.Globe)
            {
                unit.RemoveBuff(buff);
            }
        }
    }

    public override void OnRemove(BattleUnit unit)
    {
    }

    public override void OnOwnerDeath(BattleUnit unit)
    {
    }

    public override void OnTurnStart(BattleUnit unit)
    {
        remainingTurns--;
        if (remainingTurns <= 0)
        {
            unit.RemoveBuff(this);
        }
    }

    public override void AddStacks(int additionalStacks)
    {
        Stacks += additionalStacks;
        remainingTurns = 2;
    }

    public override void OnTurnEnd(BattleUnit unit)
    {
    }

    public override void OnAttack(BattleUnit unit)
    {
    }

    public override void OnAttacked(BattleUnit unit)
    {
        owner.GetArmor(Stacks);
    }

    public override void OnDamageTaken(BattleUnit unit)
    {
    }
}
