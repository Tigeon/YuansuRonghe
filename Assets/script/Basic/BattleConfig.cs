using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle Config")]
public class BattleConfig : ScriptableObject
{
    [SerializeField] UnitData Enemy1;
    [SerializeField] UnitData Enemy2;
    [SerializeField] UnitData Enemy3;
    [SerializeField] UnitData Enemy4;

    public UnitData GetEnemy(int x){
        switch(x){
            case 0:
                return Enemy1;
            case 1:
                return Enemy2;
            case 2:
                return Enemy3;
            case 3:
                return Enemy4;
            default:
                return null;
        }
    }
}
