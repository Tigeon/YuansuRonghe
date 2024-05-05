using UnityEngine;

[CreateAssetMenu(fileName = "AddBuffSpell", menuName = "Spells/加buff法术")]

public class AddBuffSpell : Spells
{
    public Buff buff;
    public override void Spell(BattleUnit target){
        if (target != null)
        {
            Buff newBuff = Instantiate(buff);
            newBuff.owner = target;
            target.AddBuff(newBuff);
        }else{
            if (targetPositions == 0){
                Buff newBuff = Instantiate(buff);;
                newBuff.owner = BattleControler.Player;
                BattleControler.Player.AddBuff(newBuff);
            }
            else{
                for (int i = 0; i < 4; i++)
                {
                    if (CanAffectPosition(i))
                    {
                        BattleUnit unit = BattleField.Instance.GetUnitAtPosition(i);
                        if (unit != null)
                        {
                            Buff newBuff = Instantiate(buff);
                            newBuff.owner = unit;
                            unit.AddBuff(newBuff);
                        }
                    }
                }
            }
        }
    }
}
