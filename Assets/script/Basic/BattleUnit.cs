using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class BattleUnit : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] public int UnitID;
    [SerializeField] public string unitName;
    [TextArea(7, 7)] [SerializeField] public string info = "";
    [SerializeField] public SpriteRenderer UnitSpriteRenderer; // 用于2D单位显示

    [Header("Status")]
    [SerializeField] public int Hp;
    [SerializeField] public int MaxHp;
    [SerializeField] public int CurrentArmor = 0;
    [SerializeField] public int HpRecover = 0;

    [SerializeField] public TextMeshProUGUI hpText; 
    [SerializeField] public TextMeshProUGUI armorText;
 
    [SerializeField] public List<Buff> BuffList = new List<Buff>();
    [SerializeField] private List<GameObject> BuffUIList = new List<GameObject>();
    [SerializeField] private Transform BuffUIParent;
    [SerializeField] private GameObject buffIconPrefab;
    public BattlePosition CurrentPosition;
    [HideInInspector]public UnitData UnitData;
    public bool isFrozen = false;

    public bool Alive(){ return Hp > 0; }

    public abstract void SetUp(UnitData unitData);

    public void Reset()
    {
        this.CurrentArmor = 0;
        this.HpRecover = 0;
        this.BuffList.Clear();
        RefreshData();
    }

    public void RefreshData()
    {
        hpText.text = $"{Hp}/{MaxHp}";
        if (CurrentArmor > 0)
        {
            armorText.gameObject.SetActive(true);
            armorText.text = $"{CurrentArmor}";
        }
        else
        {
            armorText.gameObject.SetActive(false);
        }
    }

    public abstract void GetArmor(int armor);

    public abstract void OnAttacked(int damage);

    public abstract void TakeDamage(int damage);

    public abstract void Heal(int heal);
    
    public abstract void Dead();
    
    public abstract void StartTurn();

    public abstract void EndTurn();

    public int ExtraDamage;
    public int ExtraBonus;

    public abstract void Attack(BattleUnit target , int baseDamage);


    public void AddBuff(Buff newBuff)
    {
        // 检查是否已存在相同的Buff
        foreach (Buff existingBuff in BuffList)
        {
            if (existingBuff.GetBuffName() == newBuff.GetBuffName())
            {
                // 如果找到同名的Buff，增加层数并退出方法
                existingBuff.AddStacks(newBuff.Stacks);
                Debug.Log("Buff stacked: " + existingBuff.GetBuffName() + " (" + existingBuff.Stacks + ")");
                return;
            }
        }
        Buff buffClone = newBuff.Clone();
        // 如果没有找到同名的Buff，创建新的Buff图标
        GameObject icon = Instantiate(buffIconPrefab, BuffUIParent);
        BuffIcon buffIconScript = icon.GetComponent<BuffIcon>();
        if (buffIconScript != null)
        {
            buffIconScript.Initialize(buffClone);
        }
        BuffUIList.Add(icon);
        
        BuffList.Add(buffClone);
        buffClone.OnApply(this);
    }

    public void UpdateBuffIcon()
    {
        foreach (Transform child in BuffUIParent)
        {
            BuffIcon iconScript = child.GetComponent<BuffIcon>();
            if (iconScript != null)
            {
                iconScript.UpdateStacksDisplay();  // 假设BuffIcon有方法来更新显示层数
                break;
            }
        }
    }

    public void RemoveBuff(Buff buff)
    {
        buff.OnRemove(this);
        // 找到对应的Buff图标并销毁
        foreach (Transform child in BuffUIParent)
        {
            BuffIcon buffIcon = child.GetComponent<BuffIcon>();
            if (buffIcon != null && buffIcon.buff == buff)
            {
                BuffUIList.Remove(buffIcon.gameObject);
                Destroy(child.gameObject);
                break;
            }
        }
        BuffList.Remove(buff);

    }
}
