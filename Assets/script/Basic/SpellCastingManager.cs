using System.Collections.Generic;
using UnityEngine;


public class SpellCastingManager : MonoBehaviour
{
    public static SpellCastingManager Instance;

    private List<QueuedSpell> queuedSpells = new List<QueuedSpell>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void UpdateSpells()
    {
        for (int i = queuedSpells.Count - 1; i >= 0; i--)
        {
            queuedSpells[i].remainingTurns--;
            if (queuedSpells[i].remainingTurns <= 0)
            {
                Debug.Log("Casting spell: " + queuedSpells[i].spell.Name);
                queuedSpells[i].spell.Spell(queuedSpells[i].target);
                queuedSpells.RemoveAt(i);
            }
        }
    }

    public void AddSpellToQueue(Spells spell, BattleUnit target, int turns)
    {
        queuedSpells.Add(new QueuedSpell { spell = spell, target = target, remainingTurns = turns });
        Debug.Log("Added spell to queue: " + spell.Name + " for " + turns + " turns");
    }

    private class QueuedSpell
    {
        public Spells spell;
        public BattleUnit target;
        public int remainingTurns;
    }
}
