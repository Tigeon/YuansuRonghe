using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UnitActions/Summon", fileName = "Summon")]
public class Summon : UnitAction
{
    [SerializeField] UnitData minion;
    [SerializeField] int minionCount;
    enum SummonPosition { Front = 0, Back = 3 }
    [SerializeField] SummonPosition summonPosition;

    public override void OnAction()
    {
        for (int i = 0; i < minionCount; i++)
        {
            SummonMinions();
        }
    }
    public void SummonMinions()
    {
        Enemy minionUnit = BattleControler.Instance.GenerateEnemy(minion, (int)summonPosition);
        MinionBuff minionBuff = (MinionBuff)BuffManager.Instance.GetBuff<MinionBuff>();
        if (minionUnit != null){
            minionBuff.OnApply(BattleControler.Instance.ActingUnit);
            minionUnit.AddBuff(minionBuff);
            minionBuff.owner = minionUnit;
        }   

    }
}
