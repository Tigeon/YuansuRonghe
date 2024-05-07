using System.Collections;
using UnityEngine;

[System.Serializable]
public enum buffType{ Buff, Debuff, Const, Globe, Other};

[System.Serializable]
public abstract class Buff: ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] private string buffName = "";
    [TextArea(7,7)][SerializeField] private string info = "";  // 详细描述
    [SerializeField] public int Stacks = 1;
    [SerializeField] private Sprite image;  // 存储图标
    [SerializeField] private buffType buffType;
    [SerializeField] public BattleUnit owner;   // 拥有该 Buff 的单位

    public string GetInfo() { return info; }
    public Sprite GetImage() { return image; }
    public string GetBuffName() { return buffName; }
    public buffType GetBuffType() { return buffType; }
    public abstract void OnApply(BattleUnit unit);
    public abstract void OnRemove(BattleUnit unit);
    public abstract void OnOwnerDeath(BattleUnit unit);
    public abstract void OnTurnStart(BattleUnit unit);
    public abstract void OnTurnEnd(BattleUnit unit);
    public abstract void OnAttack(BattleUnit unit);
    public abstract void OnAttacked(BattleUnit unit);
    public abstract void AddStacks(int additionalStacks);
    public abstract void OnDamageTaken(BattleUnit unit);

    public Buff Clone()
    {
        return Instantiate(this);
    }
}
