using UnityEngine;

[CreateAssetMenu(fileName = "AttackSpell", menuName = "Spells/Attack")]

public class AttackSpell : Spells
{
    public int Value;
    public override void Spell(BattleUnit target){
        if(target != null){
            BattleControler.Player.Attack(target, Value);
        }
        //如果没有目标，攻击所有范围内的敌人
        else{
            Debug.Log("Attacking all enemies");
            for (int i = 0; i < 4; i++)
            {
                if (CanAffectPosition(i))
                {
                    Debug.Log("Attacking position " + i);
                    BattleUnit unit = BattleField.Instance.GetUnitAtPosition(i);
                    Debug.Log("Unit at position " + i + ": " + unit);
                    if (unit != null)
                    {
                        BattleControler.Player.Attack(unit, Value);
                    }
                }
            }
        }
    }
}
