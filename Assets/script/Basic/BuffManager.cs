using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    // 单例模式
    public static BuffManager Instance { get; private set; }

    // 存储所有 Buff ScriptableObjects 的列表
    public List<Buff> availableBuffs;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject); // 使该管理器在场景加载时不被销毁
        }
    }

    public Buff GetBuffByName(string buffName)
    {
        foreach (Buff buff in availableBuffs)
        {
            if (buff.GetBuffName() == buffName)
            {
                return Instantiate(buff);  // 创建并返回 Buff 的一个新实例
            }
        }
        Debug.LogWarning("Buff named " + buffName + " not found.");
        return null;
    }

    public Buff GetBuff<T>() where T : Buff
    {
        foreach (Buff buff in availableBuffs)
        {
            if (buff is T)
            {
                return Instantiate(buff);  // 创建并返回 Buff 的一个新实例
            }
        }
        Debug.LogWarning("Buff of type " + typeof(T) + " not found.");
        return null;
    }


    // 方法：应用 Buff 到指定的 BattleUnit
    public void ApplyBuffToUnit(BattleUnit unit, Buff buff)
    {
        if (unit != null && buff != null)
        {
            Buff newBuff = Instantiate(buff); // 创建 Buff 实例
            unit.AddBuff(newBuff);
        }
    }
}

