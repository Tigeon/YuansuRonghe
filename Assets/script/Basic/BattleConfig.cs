using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle Config")]
public class BattleConfig : ScriptableObject
{
    [SerializeField] UnitData[] Enemies = new UnitData[4];

    [SerializeField] Sprite Background;

    public UnitData GetEnemy(int x){
        switch(x){
            case 0:
                return Enemies[0];
            case 1:
                return Enemies[1];
            case 2:
                return Enemies[2];
            case 3:
                return Enemies[3];
            default:
                return null;
        }
    }
}
