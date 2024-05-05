using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardController : MonoBehaviour
{
    // Reference to the reward prefab
    public GameObject rewardPrefab;

    // Reference to the GridLayoutGroup
    public GridLayoutGroup rewardGrid;

    // Singleton instance
    public static RewardController Instance { get; private set; }

    private void Awake()
    {
        // Ensure only one instance of RewardController exists
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

    // Generate rewards after a battle
    public void GenerateBattleRewards()
    {
        // Array of possible rewards and their probability weights
        RewardType[] rewardTypes = { RewardType.Gold, RewardType.Card, RewardType.Item };
        int[] weights = { 50, 30, 20 }; // Weights corresponding to the likelihood of each reward type
        int totalWeight = 0;
        foreach (var weight in weights)
            totalWeight += weight;

        // Generating three rewards based on weights
        for (int i = 0; i < 3; i++)
        {
            RewardType chosenRewardType = ChooseRewardType(rewardTypes, weights, totalWeight);
            GameObject rewardObject = Instantiate(rewardPrefab, rewardGrid.transform);
            Reward reward = rewardObject.GetComponent<Reward>();
            SetupRewardContent(reward, chosenRewardType);
        }
    }

    private RewardType ChooseRewardType(RewardType[] types, int[] weights, int totalWeight)
    {
        int randomNumber = Random.Range(0, totalWeight);
        for (int i = 0; i < types.Length; i++)
        {
            if (randomNumber < weights[i])
                return types[i];
            randomNumber -= weights[i];
        }
        return types[0]; // Default return if something goes wrong
    }

    private void SetupRewardContent(Reward reward, RewardType type)
    {
        switch (type)
        {
            case RewardType.Gold:
                reward.SetContent(type, CalculateGoldCount());
                break;
            case RewardType.Card:
                reward.SetContent(type, CalculateCardRank());
                break;
            case RewardType.Item:
                reward.SetContent(type, CalculateItemRank());
                break;
        }
    }

    private int CalculateGoldCount(){
        return Random.Range(100, 201);  // More realistic reward amounts
    }

    private int CalculateCardRank(){
        // Assuming ranks range from 0 (common) to 3 (legendary)
        return Random.Range(0, 4);
    }

    private int CalculateItemRank(){
        // Assuming two possible ranks for items: 0 (common), 1 (rare)
        return Random.Range(0, 2);
    }
}
