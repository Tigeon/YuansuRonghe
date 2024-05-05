using UnityEngine;
using System.Collections.Generic;


public class Hero : BattleUnit
{

    // Setting up the Hero with data from UnitData
    public override void SetUp(UnitData unitData)
    {
        UnitData = unitData;
        UnitID = unitData.GetId();
        unitName = unitData.GetName();
        MaxHp = unitData.GetMaxHp();
        Hp = MaxHp;
        info = unitData.GetInfo();
        UnitSpriteRenderer.sprite = Resources.Load<Sprite>($"Unit/{unitData.GetImageName()}");
        //把英雄的数据显示在UI上,然后删掉原来的UI
        var temp = hpText;
        hpText = UIManager.Instance.PlayerHpText;
        Destroy(temp.gameObject);
        RefreshData();
    }

    // Increasing armor
    public override void GetArmor(int armor)
    {
        this.CurrentArmor += armor;
        RefreshData();
    }

    public override void OnAttacked(int damage)
    {
        foreach (var buff in BuffList)
        {
            buff.OnAttacked(this);
        }

        if (CurrentArmor > 0)
        {
            CurrentArmor -= damage;
            if (CurrentArmor < 0)
            {
                TakeDamage(CurrentArmor);
                CurrentArmor = 0;
            }
        }
        RefreshData();
    }

    // Taking damage and handling armor
    public override void TakeDamage(int damage)
    {
        Hp -= damage;
        if (Hp <= 0)
        {
            Hp = 0;
            Dead();
        }
        RefreshData();
    }

    // Healing the Hero
    public override void Heal(int heal)
    {
        Hp = Mathf.Min(MaxHp, Hp + heal);
        RefreshData();
    }

    // Handling Hero's death
    public override void Dead()
    {
        Debug.Log("Hero is dead");
        CurrentPosition.ClearUnit();
        Destroy(gameObject);
    }

    public override void StartTurn()
    {
        foreach (var buff in BuffList)
        {
            buff.OnTurnStart(this);
        }
        CurrentArmor = 0;
        RefreshData();
    }

    public override void EndTurn()
    {
        // 创建BuffList的副本
        List<Buff> buffsCopy = new List<Buff>(BuffList);

        // 在副本上进行遍历
        foreach (var buff in buffsCopy)
        {
            buff.OnTurnEnd(this);
        }
    }
    public override void Attack(BattleUnit target, int baseDamage)
    {
        int totalDamage = baseDamage; // 假设有基础伤害值

        // 调用所有 Buff 的 OnAttack 方法
        foreach (Buff buff in BuffList)
        {
            buff.OnAttack(this); // 此处传递的是攻击者
        }
        totalDamage += ExtraDamage;

        // 实际造成伤害
        target.TakeDamage(totalDamage);
        
    }

}
