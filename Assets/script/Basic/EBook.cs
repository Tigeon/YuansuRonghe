using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class EBook : MonoBehaviour
{
    public List<Element> hand = new List<Element>();
    public List<Element> discardPile = new List<Element>();
    public const int MAX_DISCARD_LIMIT = 10;

    public Transform handTransform;
    public GameObject elementPrefab;

    public Transform discardPileTransform;

    public static EBook Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void AddElementToHand(Element element)
    {
        if (hand.Count >= 5) return;
        hand.Add(element);
        NewElemntCreate();
        TryCastSpell();
    }

    private void TryCastSpell()
    {
        Spells spell = DataBase.FindSpell(hand);
        if (spell != null)
        {
            Debug.Log("Spell found: " + spell.Name);
            SpellBook.Instance.ActivateSpell(spell);
        }
        else
        {
            SpellBook.Instance.NoSpellFind();
        }
    }
    public IEnumerator MoveElementsToDiscardPile()
    {
        List<Element> toDiscard = new List<Element>(hand);

        int i = 0;
        while (toDiscard.Count > 0)
        {
            Element currentElement = toDiscard[0];

            // 如果弃牌区不为空，检查反应
            if (discardPile.Count > 0 && Element.CanReact(discardPile[discardPile.Count - 1], currentElement) && i <= discardPileTransform.childCount - 1)
            {


                Transform lastElementTransform = discardPileTransform.GetChild(discardPileTransform.childCount - 1 - i);
                Transform currentElementTransform = handTransform.GetChild(i); // 注意这里可能会出现问题，如果handTransform没有子元素

                // 移动当前元素到弃牌区最后一个元素位置
                yield return MoveElementToPosition(currentElementTransform, lastElementTransform.position);

                // 触发反应
                Element.React(discardPile[discardPile.Count - 1].elementType, currentElement.elementType);
                discardPile.RemoveAt(discardPile.Count - 1); // 移除反应的元素
                toDiscard.RemoveAt(0);
                
                Destroy(lastElementTransform.gameObject); // 安全地销毁UI对象
                Destroy(currentElementTransform.gameObject); // 安全地销毁UI对象
                Debug.Log("Reaction happened");
                i++;
            }
            else
            {
                // 如果没有反应，将当前元素和剩余元素移动到弃牌区
                discardPile.Add(currentElement);
                foreach (Element remaining in toDiscard)
                {
                    discardPile.Add(remaining);
                    // 假设存在将元素直接从手牌UI移动到弃牌UI的方法
                    MoveChildren(handTransform, discardPileTransform);
                }
                toDiscard.Clear();
            }

            // 检查是否达到弃牌区的上限
            if (discardPile.Count >= MAX_DISCARD_LIMIT)
            {
                EndTurn();
                yield break;
            }
        }
        hand.Clear(); // 清空手牌
    }

    private IEnumerator MoveElementToPosition(Transform element, Vector3 targetPosition)
    {
        while (Vector3.Distance(element.position, targetPosition) > 0.05f)
        {
            element.position = Vector3.MoveTowards(element.position, targetPosition, Time.deltaTime * 5); // 可以调整移动速度
            yield return null;
        }
    }

    public void MoveChildren(Transform sourceParent, Transform targetParent)
    {
        int childCount = sourceParent.childCount;

        // 使用循环来遍历所有子物体
        for (int i = 0; i < childCount; i++)
        {
            // 因为每次从 sourceParent 移除一个子物体，sourceParent 的 childCount 就会减少，
            // 所以始终取第一个索引为0的子物体
            Transform child = sourceParent.GetChild(0);
            child.SetParent(targetParent);

            // 可选：如果你也想重置子物体的本地位置、旋转和缩放
            child.localPosition = Vector3.zero;
            child.localRotation = Quaternion.identity;
            child.localScale = Vector3.one;
        }
    }


    private void EndTurn()
    {
        // 结束回合的逻辑
        TurnSystem.Instance.EndMyTurn();
    }

    public void ClearHand()
    {
        hand.Clear();
        // 更新手牌UI的逻辑
        foreach (Transform child in handTransform)
        {
            Destroy(child.gameObject);
        }
        SpellBook.Instance.ClearSpellBook();
    }

    public void NewElemntCreate()
    {
        // 更新手牌UI的逻辑
        var NewElemnt = Instantiate(elementPrefab, handTransform);
        NewElemnt.transform.localScale = new Vector3(1, 1, 1);
        NewElemnt.transform.SetParent(handTransform);
        NewElemnt.GetComponent<SpriteRenderer>().sprite = hand[hand.Count - 1].Icon;
    }

    private void discardPileClear()
    {
        discardPile.Clear();
        // 更新废弃牌UI的逻辑
        foreach (Transform child in discardPileTransform)
        {
            Destroy(child.gameObject);
        }
    }

    public void Reset(){
        discardPileClear();
        ClearHand();
    }

}
