using UnityEngine;

[CreateAssetMenu(fileName = "BlockSpell", menuName = "Spells/Block")]

public class BlockSpell : Spells
{
    public int Value;
    public override void Spell(BattleUnit target){
        if(target != null){
            target.GetArmor(Value);
        }else{
            BattleControler.Player.GetArmor(Value);
        }
    }
}