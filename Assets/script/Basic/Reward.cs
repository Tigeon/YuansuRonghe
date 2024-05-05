using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum RewardType
{
    Gold,
    Card,
    Item,
    Health,
    Element  // 新增元素奖励类型
}

public class Reward : MonoBehaviour
{
    public Sprite rewardContentSprite;

    // Sprites for different reward types
    public Sprite moneySprite;
    public Sprite cardSprite;
    public Sprite itemSprite;
    public Sprite healthSprite;
    public Sprite elementSprite;  // 新增元素奖励的精灵

    private RewardType rewardType;
    private int rewardAmount;

    public void Claim()
    {
        ProcessReward(rewardType, rewardAmount);
        Destroy(gameObject);
    }

    public void SetContent(RewardType type, int amount)
    {
        rewardType = type;
        rewardAmount = amount;
        switch (type)
        {
            case RewardType.Gold:
                rewardContentSprite = moneySprite;
                break;
            case RewardType.Card:
                rewardContentSprite = cardSprite;
                break;
            case RewardType.Item:
                var item = ItemDatabase.Instance.GetItem(amount);
                rewardContentSprite = item != null ? item.itemSprite : itemSprite;
                break;
            case RewardType.Health:
                rewardContentSprite = healthSprite;
                break;
            case RewardType.Element:
                rewardContentSprite = elementSprite;  // 设置元素奖励的精灵
                break;
            default:
                Debug.LogError("Unknown reward type: " + type);
                break;
        }
        GetComponent<Image>().sprite = rewardContentSprite;
    }

    private void ProcessReward(RewardType type, int amount)
    {
        switch (type)
        {
            case RewardType.Gold:
                BattleData.Instance.ChangeGold(amount);
                break;
            case RewardType.Item:
                Inventory.Instance.AddItem(amount);
                break;
            case RewardType.Health:
                BattleControler.Player.Heal(amount);
                break;
            case RewardType.Element:
                //ElementDiscovery.Instance.DiscoverElements(amount);
                break;
            default:
                Debug.LogError("Unknown reward type: " + type);
                break;
        }
    }
}
