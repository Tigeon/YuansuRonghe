using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleControler : MonoBehaviour
{
    public List<BattleConfig> NormalBattleConfigs;
    public List<BattleConfig> EliteBattleConfigs;
    public List<BattleConfig> BossBattleConfigs;
    private int currentBattleID = 0;
    public Enemy EnemyModel;
    public Hero PlayerModel;

    public static BattleUnit Player { get; private set; }

    public UnitData PlayerUnitData;

    [SerializeField] public List<Enemy> enemyList = new List<Enemy>();

    public static BattleControler Instance { get; private set; }
    private BattleField battleField;
    public BattleUnit ActingUnit;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        battleField = BattleField.Instance;
    }

    public void Start()
    {
        StartNormalBattle(0);
    }

    public void StartNormalBattle(int battleID)
    {
        Reset();
        GeneratePlayer(); // 生成玩家单位
        var battle = NormalBattleConfigs[currentBattleID];
        for (int i = 0; i < 4; i++)
        {
            if (battle.GetEnemy(i) == null) continue;
            GenerateEnemy(battle.GetEnemy(i), i);
        }
        TurnSystem.Instance.StartPlayerTurn();
    }

    public void StartEliteBattle(int battleID)
    {
        Reset();
        GeneratePlayer(); // 生成玩家单位
        var battle = EliteBattleConfigs[currentBattleID];
        for (int i = 0; i < 4; i++)
        {
            if (battle.GetEnemy(i) == null) continue;
            GenerateEnemy(battle.GetEnemy(i), i);
        }
        TurnSystem.Instance.StartPlayerTurn();
    }

    public void StartBossBattle(int battleID)
    {
        Reset();
        GeneratePlayer(); // 生成玩家单位
        var battle = BossBattleConfigs[currentBattleID];
        for (int i = 0; i < 4; i++)
        {
            if (battle.GetEnemy(i) == null) continue;
            GenerateEnemy(battle.GetEnemy(i), i);
        }
        TurnSystem.Instance.StartPlayerTurn();
    }

    // 重置战斗环境
    public void Reset()
    {
        foreach (var unit in enemyList)
        {
            unit.Reset();
        }
        enemyList.Clear();
    }

    // 战斗结束
    public void BattleEnd()
    {
        UIManager.Instance.DisableButtons();
        VictoryUI.Instance.Victory(); // 假设 VictoryUI 有 DisplayVictory 方法
    }

    // 敌人被击败
    public void EnemyKilled(Enemy enemy)
    {
        enemyList.Remove(enemy);
        if (enemyList.Count == 0)
            BattleEnd();
    }

    // 模拟创建敌人单位
    public Enemy GenerateEnemy(UnitData EnemyData, int position)
    {
        // 创建单位的逻辑，这需要与实际游戏环境的实现相适应
        Enemy Unit = Instantiate(EnemyModel, Vector3.zero, Quaternion.identity);
        Unit.SetUp(EnemyData);
        if(BattleField.Instance.PlaceUnitOnBattlefield(Unit, position)){
            enemyList.Add(Unit);
            return Unit;
        }
        Destroy(Unit.gameObject);
        return null;
        
    }


    private void GeneratePlayer()
    {
        // 创建单位的逻辑，这需要与实际游戏环境的实现相适应
        var Unit = Instantiate(PlayerModel, Vector3.zero, Quaternion.identity);
        Unit.SetUp(PlayerUnitData);
        Player = Unit;
        BattleField.Instance.PlaceUnitOnBattlefield(Unit, -1);
    }
}
