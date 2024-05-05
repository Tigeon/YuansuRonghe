using UnityEngine;

public class BattleData : MonoBehaviour
{
    public int enemyKilledCount; // 击杀敌人的数量
    public int turnCount; // 回合数
    public int currentCost; // 当前消耗
    public int maxCost; // 最大消耗
    public int NumberOfCardsDrawEveryTurn; // 每回合抽牌数
    public int NumberOfExtraCardsDrawEveryTurn; // 每回合额外抽牌数
    public int NumberOfCardsShowInReward; // 奖励中展示的卡牌数
    public int Gold; // 金币

    public static BattleData Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            ResetAll();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetAll()
    {   
        enemyKilledCount = 0;
        turnCount = 0;
        currentCost = 0;
        NumberOfCardsDrawEveryTurn = 4;
        NumberOfExtraCardsDrawEveryTurn = 0;
        NumberOfCardsShowInReward = 3;
        Gold = 0;
    }

    public void ChangeGold(int amount)
    {
        Gold += amount;
        if (Gold < 0)
            Gold = 0;
        UpdateGoldDisplay(); // 假设存在这样一个方法来更新UI显示
    }

    private void UpdateAPDisplay()
    {
        // 更新UI显示AP的逻辑
    }

    private void UpdateGoldDisplay()
    {
        // 更新UI显示Gold的逻辑
    }
}
