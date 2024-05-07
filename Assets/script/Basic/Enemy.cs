using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BattleUnit
{
    [SerializeField] private ActionIcon actionIcon;

    [SerializeField] public List<UnitAction> actionList = new List<UnitAction>();
    private int currentActionIndex = 0;


    public override void SetUp(UnitData unitData)
    {
        UnitData = unitData;
        UnitID = unitData.GetId();
        unitName = unitData.GetName();
        MaxHp = unitData.GetMaxHp();
        Hp = MaxHp;
        info = unitData.GetInfo();
        actionList = unitData.GetActionList();
        UnitSpriteRenderer.sprite = unitData.GetImage();
        

        actionIcon.UpdateUI(actionList[currentActionIndex]);
        RefreshData();
    }

    public override void Dead()
    {
        BattleControler.Instance.EnemyKilled(this);
        
        Destroy(gameObject);
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


    public override void GetArmor(int armor)
    {
        CurrentArmor += armor;
        RefreshData();
    }

    public override void Heal(int heal)
    {
        Hp += heal;
        if (Hp > MaxHp)
        {
            Hp = MaxHp;
        }
        RefreshData();
    }
    
    public override void StartTurn()
    {
        foreach (var buff in BuffList)
        {
            buff.OnTurnStart(this);
        }
        CurrentArmor = 0;
    }

    public IEnumerator Act()
    {
        BattleControler.Instance.ActingUnit = this;
        Debug.Log(unitName + " Act");
        if(isFrozen){
            Debug.Log(unitName + " is frozen");
        }else{
            actionList[currentActionIndex].OnAction();
            NextAction();
        }
    
        RefreshData();
        actionIcon.UpdateUI(actionList[currentActionIndex]);
        yield return actionList[currentActionIndex].WaitSeconds;
        
    }

    public void NextAction()
    {
        currentActionIndex++;
        if (currentActionIndex >= actionList.Count)
        {
            currentActionIndex = 0;
        }
    }
    public override void EndTurn()
    {
        for (int i = BuffList.Count - 1; i >= 0; i--)
        {
            BuffList[i].OnTurnEnd(this);
        }

        RefreshData();
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
        totalDamage = totalDamage * (1 + ExtraBonus / 100);

        // 实际造成伤害
        target.TakeDamage(totalDamage);
        
    }


}
