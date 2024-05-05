using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePosition : MonoBehaviour
{
    public int position;
    public BattleUnit CurrentUnit;
    public GameObject UI1;
    public bool IsOccupied()
    {
        return CurrentUnit != null;
    }

    public void SetPosition(int position)
    {
        this.position = position;
    }

    public void PlaceUnit(BattleUnit unit)
    {
        CurrentUnit = unit;
    }

    public BattleUnit GetUnit()
    {
        return CurrentUnit;
    }

    public void ClearUnit()
    {
        CurrentUnit = null;
    }

    public int GetPosition()
    {
        return position;
    }
}
