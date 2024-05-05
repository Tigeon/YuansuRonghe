using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro; // Add this line to use TextMeshPro
using System.Linq;


public class UIManager : MonoBehaviour
{
    // Singleton instance
    public static UIManager Instance { get; private set; }

    // UI elements
    public TextMeshProUGUI PlayerHpText;
    public TextMeshProUGUI GoldText;
    public GameObject itemPrefab;
    public GameObject itemUIParent;
    // Add references to other UI elements as needed
    public GameObject TurnEndButton;

    public List<Item> Items;

    private void Awake()
    {
        // Implement singleton pattern
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

    public void UpdateHP()
    {
        var hp = BattleControler.Player.Hp;
        var maxHp = BattleControler.Player.MaxHp;
        PlayerHpText.text = $"{hp}/{maxHp}";
    }

    public void UpdateGold()
    {
        var Gold = BattleData.Instance.Gold;
        GoldText.text = $"{Gold}";
    }

    public void AddItem(Item Item){
        var itemUI = Instantiate(itemPrefab, itemUIParent.transform);
        itemUI.GetComponent<Image>().sprite = Item.itemSprite;
        Items.Add(Item);
    }

    public void RemoveItem(Item Item){
        foreach (Item item in Items)
        {
            if (item.Id == Item.Id)
            {
                Items.Remove(item);
                break;
            }
        }
    }

    public void DisableButtons()
    {
        TurnEndButton.SetActive(false);
    }

}

