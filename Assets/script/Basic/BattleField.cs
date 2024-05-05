using UnityEngine;
using System.Collections.Generic;

public class BattleField : MonoBehaviour
{
    [SerializeField] private RectTransform battlefieldZone; // 战场区域的RectTransform组件

    public static BattleField Instance { get; private set; }

    public List<BattlePosition> EnemyBattlePositions = new List<BattlePosition>(); // 战场区域的BattlePosition组件列表
    public BattlePosition PlayerBattlePosition; // 玩家的BattlePosition组件

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // 检查鼠标位置是否在战场区域内
    public bool IsMouseInBattlefieldZone()
    {
        if (battlefieldZone == null) return false;

        // 将屏幕坐标转换为Canvas坐标
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            battlefieldZone,
            Input.mousePosition,
            null,
            out Vector2 localMousePosition
        );

        // 检查鼠标位置是否在战场区域的矩形范围内
        bool isInside = battlefieldZone.rect.Contains(localMousePosition);

        return isInside;
    }
    
   public bool PlaceUnitOnBattlefield(BattleUnit unit, int index)
    {
        if (index == -1)
        {
            PlayerBattlePosition.PlaceUnit(unit);
            unit.transform.SetParent(PlayerBattlePosition.transform);
            unit.transform.localPosition = Vector3.zero;
            return true;
        }

        BattlePosition position = EnemyBattlePositions[index];

        if (position.IsOccupied())
        {
            // 找到空位或需要移动的方向
            Debug.Log("Unit is already in position " + index);
            int shiftDirection = FindShiftDirection(index);
            if(shiftDirection == 0){
                return false;
            }
            ShiftUnits(index, shiftDirection);
        }

        position.PlaceUnit(unit);
        unit.CurrentPosition = position;
        unit.transform.SetParent(position.transform);
        unit.transform.localPosition = Vector3.zero;
        return true;
    }

    private int FindShiftDirection(int index)
    {
        int leftEmpty = -1, rightEmpty = -1;

        // 向左查找空位
        for (int i = index - 1; i >= 0; i--)
        {
            if (!EnemyBattlePositions[i].IsOccupied())
            {
                leftEmpty = i;
                break;
            }
        }

        // 向右查找空位
        for (int i = index + 1; i < 4; i++)
        {
            if (!EnemyBattlePositions[i].IsOccupied())
            {
                rightEmpty = i;
                break;
            }
        }

        // 选择最近的空位方向
        if (leftEmpty != -1 && (rightEmpty == -1 || index - leftEmpty <= rightEmpty - index))
            return -1;  // 向左移动
        else if (rightEmpty != -1)
            return 1;   // 向右移动

        return 0;  // 没有空位，理论上不应该发生，可能需要处理错误或者异常情况
    }

    private void ShiftUnits(int startIndex, int direction)
    {
        if (direction == 0) return;

        int pos = startIndex;
        // 尝试向指定方向移动，直到找到空位或超出边界

        BattlePosition current = EnemyBattlePositions[pos];
        BattlePosition next = EnemyBattlePositions[pos + direction];
        //如果下一个位置已经被占据,把下一个位置的单位移动到下下个位置
        if (next.IsOccupied())
        {
            ShiftUnits(pos + direction , direction);
        }
        
        // 逻辑上移动单位
        next.PlaceUnit(current.GetUnit());
        current.ClearUnit();

        // 更新单位的实际位置
        
        next.GetUnit().transform.SetParent(next.transform);
        next.GetUnit().transform.localPosition = Vector3.zero;
        
        next.GetUnit().CurrentPosition = next;
    }
    

// 返回临近的敌人
    public BattleUnit GetNearbyEnemy(BattlePosition pos)
    {

        int currentIndex = EnemyBattlePositions.IndexOf(pos);  // 获取当前位置的索引

        List<BattleUnit> nearbyEnemies = new List<BattleUnit>();  // 存储找到的相邻敌人

        // 检查左边的敌人
        if (currentIndex > 0 && EnemyBattlePositions[currentIndex - 1].IsOccupied())  // 确保索引有效并且位置被占用
        {
            nearbyEnemies.Add(EnemyBattlePositions[currentIndex - 1].GetUnit());
        }

        // 检查右边的敌人
        if (currentIndex < EnemyBattlePositions.Count - 1 && EnemyBattlePositions[currentIndex + 1].IsOccupied())  // 确保索引有效并且位置被占用
        {
            nearbyEnemies.Add(EnemyBattlePositions[currentIndex + 1].GetUnit());
        }

        // 从找到的相邻敌人中随机选择一个
        if (nearbyEnemies.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, nearbyEnemies.Count);
            return nearbyEnemies[randomIndex];
        }
        Debug.Log("No nearby enemy found");

        return null;  // 如果没有找到相邻的敌人，返回 null
    }

    public BattleUnit GetUnitAtPosition(int position)
    {
        if (position == -1)
        {
            return PlayerBattlePosition.GetUnit();
        }

        if (position >= 0 && position < EnemyBattlePositions.Count)
        {
            return EnemyBattlePositions[position].GetUnit();
        }

        return null;
    }

    public void ShowBattlefieldUI1(Spells spell)
    {
        foreach (BattlePosition position in EnemyBattlePositions)
        {
            if (spell.CanAffectPosition(position.GetPosition()))
            {
                position.UI1.SetActive(true);  // Activate the UI1 GameObject
            }
            else
            {
                position.UI1.SetActive(false); // Optionally deactivate UI1 if not affected
            }
        }
    }

    public void HideBattlefieldUI1()
    {
        foreach (BattlePosition position in EnemyBattlePositions)
        {
            position.UI1.SetActive(false);  // Deactivate the UI1 GameObject
        }
    }

}