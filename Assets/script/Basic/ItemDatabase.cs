using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    // Singleton instance
    public static ItemDatabase Instance { get; private set; }

    [SerializeField]
    private List<Item> itemDataList = new List<Item>();

    private void Awake()
    {
        // Ensure only one instance of ItemDatabase exists
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

    // Get an item by its ID
    public Item GetItem(int id)
    {
        if (id >= 0 && id < itemDataList.Count)
        {
            return itemDataList[id];
        }

        Debug.LogError("Item not found: " + id);
        return null;
    }
}
