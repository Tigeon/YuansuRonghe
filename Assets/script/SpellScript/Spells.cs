using System.Collections.Generic;
using UnityEngine;
using System;


[System.Serializable]
public abstract class Spells : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public List<Element> RequiredElement;
    [TextArea(7, 7)] [SerializeField] string Description;

    public enum TargetType
    {
        None,
        Enemy
    }

    [Flags]
    public enum TargetPositions
    {
        None = 0,
        Position1 = 1 << 0,  // 1
        Position2 = 1 << 1,  // 2
        Position3 = 1 << 2,  // 4
        Position4 = 1 << 3   // 8
    }

    public int chantTurns;  // 咏唱所需的回合数
    public TargetType targetType;
    public TargetPositions targetPositions;

    public abstract void Spell(BattleUnit target);

    public bool CanAffectPosition(int positionIndex)
    {
        TargetPositions positionFlag = (TargetPositions)(1 << positionIndex);
        return (targetPositions & positionFlag) == positionFlag;
    }

    public string GetDescription()
    {
        return Description;
    }

    // 处理咏唱逻辑
    public void CastSpell(BattleUnit target)
    {
        if (chantTurns > 0)
        {
            SpellCastingManager.Instance.AddSpellToQueue(this, target, chantTurns);
        }
        else
        {
            Spell(target);
        }
    }
}
