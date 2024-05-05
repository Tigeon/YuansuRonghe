using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LightningSpell", menuName = "Spells/闪电链")]
public class LightningSpell : Spells
{
    public int bounceCount = 3;  // 弹射次数
    public float damageReductionPerBounce = 1.0f;  // 每次弹射伤害减少的值
    public int Value;

    public override void Spell(BattleUnit target)
    {
        if (target == null) return;

        int currentDamage = Value;  // 初始伤害值

        // 对初始目标造成伤害
        BattleControler.Player.Attack(target, currentDamage);

        // 执行弹射逻辑
        for (int i = 0; i < bounceCount; i++)
        {
            // 寻找下一个目标
            target = BattleField.Instance.GetNearbyEnemy(target.CurrentPosition);
            if (target == null) break;

            // 减少伤害并对新目标造成伤害
            currentDamage -= (int)damageReductionPerBounce;

            if (currentDamage > 0)
            {
                BattleControler.Player.Attack(target, currentDamage);
            }
            
        }
    }

}
