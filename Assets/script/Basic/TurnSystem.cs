using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;


public class TurnSystem : MonoBehaviour
{
    public bool isMyTurn;
    public static TurnSystem Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }else
        {
            Destroy(gameObject);
        }
    }
    public int maxCost = 10;
    public Camera mainCamera;
    public GameObject TurnEndButton;

    void Update()
    {
    }

    public void EndMyTurn()
    {
        BattleControler.Player.EndTurn();
        EBook.Instance.Reset();
        isMyTurn = false;
        TurnEndButton.SetActive(false);
        StartCoroutine(PerformEnemyActions());
    }
    private IEnumerator PerformEnemyActions()
    {
        isMyTurn = false;

        // 创建敌人列表的副本
        List<Enemy> enemiesCopy = new List<Enemy>(BattleControler.Instance.enemyList);

        // 遍历副本进行行动
        foreach (Enemy enemy in enemiesCopy)
        {
            enemy.StartTurn();
        }
        
        foreach (Enemy enemy in enemiesCopy)
        {
            yield return StartCoroutine(enemy.Act());  // 等待当前敌人完成其行动
        }
        Debug.Log("All enemies have acted.");

        // 敌人回合结束
        EndEnemyTurn();
    }

    public void EndEnemyTurn()
    {
        foreach (Enemy enemy in BattleControler.Instance.enemyList)
        {
            enemy.EndTurn();
        }

        StartPlayerTurn();
    }
    
    public void StartPlayerTurn()
    {
        isMyTurn = true;
        SpellCastingManager.Instance.UpdateSpells();
        BattleControler.Player.StartTurn();
        TurnEndButton.SetActive(true);
    }
}
