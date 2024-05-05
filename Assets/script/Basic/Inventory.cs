using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    // Singleton instance
    public static Inventory Instance { get; private set; }

    private void Awake()
    {
        // Ensure only one instance of Inventory exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public List<Item> items = new List<Item>(); // 存储物品的列表

    // 向背包中添加物品
    public void AddItem(Item item)
    {
        items.Add(item);
        UIManager.Instance.AddItem(item);
    }

    public void AddItem(int id){
        Item item = ItemDatabase.Instance.GetItem(id);
        items.Add(item);
        UIManager.Instance.AddItem(item);
    }

    // 从背包中移除物品
    public void RemoveItem(Item item)
    {
        items.Remove(item);
        Debug.Log("Removed " + item.itemName);
        UIManager.Instance.RemoveItem(item);
    }

    // 检查背包中是否有特定的物品
    public bool HasItem(int id)
    {
        foreach (Item item in items)
        {
            if (item.Id == id)
            {
                return true;
            }
        }

        return false;
    }
}
