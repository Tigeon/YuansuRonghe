using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class SpellBook : MonoBehaviour
{
    public Transform spellPanel;  // UI panel to display spells
    public SpellDescription spellPrefab;  // UI prefab for displaying a spell
    private List<Element> currentElements;
    private bool isWaitingForTarget = false;
    public GameObject ProjectilePrefab;
    public Transform BattleFieldPanel;
    public static SpellBook Instance { get; private set; }

    public GameObject CastButton;
    public GameObject TargetingUI;
    public GameObject NoTargetUI;

    [HideInInspector] public Spells ActiveSpell;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }else
        {
            Destroy(gameObject);
        }
        spellPanel.gameObject.SetActive(false);
        TargetingUI.SetActive(false);
        NoTargetUI.SetActive(false);
        CastButton.SetActive(false);
        BattleField.Instance.HideBattlefieldUI1();
    }

    public void ActivateSpell(Spells spell)
    {
        spellPanel.gameObject.SetActive(true);
        NoTargetUI.SetActive(false);
        CastButton.SetActive(true);
        ActiveSpell = spell;
        // Loop through all battle positions and activate UI1 where applicable
        BattleField.Instance.ShowBattlefieldUI1(spell);

        UpdateSpellbook();
    }
    
    // 更新魔法书，显示符合当前元素条件的魔法
    public void UpdateSpellbook()
    {
        foreach (Transform child in spellPanel)
        {
            if(child.GetComponent<SpellDescription>() != null)
                Destroy(child.gameObject);
        }


        SpellDescription spellObj = Instantiate(spellPrefab, spellPanel);
        spellObj.Initialize(ActiveSpell);
        spellObj.gameObject.SetActive(true);

    }

    private BattleUnit CurrentTarget;
    public void SelectSpell()
    {
        if (isWaitingForTarget)
        {
            return;
        }
        if (ActiveSpell.targetType == Spells.TargetType.None)
        {
            // If no target is required, cast the spell immediately
            CastSpell(null);
            //SubtractElements(spell.RequiredElement);
        }
        else
        {
            TargetingUI.SetActive(true);
            StartCoroutine(WaitForTargetSelection());
        }
    }

    private IEnumerator WaitForTargetSelection()
    {
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;  // Continue waiting until mouse button is pressed
        }

        TargetingUI.SetActive(false);
        Vector3 targetPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(targetPoint, Vector2.zero);

        if (hit.collider != null)
        {
            CurrentTarget = hit.collider.GetComponent<BattleUnit>();
            if (CurrentTarget != null)
            {
                if(ActiveSpell.CanAffectPosition(CurrentTarget.CurrentPosition.position))
                {
                    var projectile=  Instantiate(ProjectilePrefab, BattleField.Instance.PlayerBattlePosition.transform.position, Quaternion.identity);
                    projectile.GetComponent<Projectile>().target = CurrentTarget.transform;
                    //把projectile放到BattleField的同级
                    projectile.transform.SetParent(BattleFieldPanel);
                }
            }
        }
    }

    private void CastSpell( BattleUnit target ){
        isWaitingForTarget = false;
        CastButton.SetActive(false);
        spellPanel.gameObject.SetActive(false);
        ActiveSpell.CastSpell(target);
        EBook.Instance.StartCoroutine(EBook.Instance.MoveElementsToDiscardPile());
    }

    public void SpellCastToCurrentTarget() {
        CastSpell(CurrentTarget);
    }

    public void NoSpellFind(){
        ActiveSpell = null;
        TargetingUI.SetActive(false);
        spellPanel.gameObject.SetActive(false);
        NoTargetUI.SetActive(true);
        CastButton.SetActive(false);
        BattleField.Instance.HideBattlefieldUI1();
    }

    public void ClearSpellBook()
    {
        spellPanel.gameObject.SetActive(false);
        ActiveSpell = null;
        NoSpellFind();
        NoTargetUI.SetActive(false);
        CastButton.SetActive(false);
        BattleField.Instance.HideBattlefieldUI1();
    }
}
