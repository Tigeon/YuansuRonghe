using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;  // 用于处理UI事件
using TMPro;

public class SpellDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public SpriteRenderer iconImage;  // 使用 UnityEngine.UI.Image 来显示法术图标
    public GameObject tooltip;  // Tooltip UI 对象
    public TextMeshProUGUI tooltipText;  // Tooltip 文本对象
    
    public TextMeshProUGUI NameText;  // 显示 Buff 名称的文本对象

    public Spells spell;  // 关联的 Spell 对象

    void Start()
    {
        if (tooltip != null)
        {
            tooltip.SetActive(false);  // 初始时隐藏 Tooltip
        }
    }

    public void Initialize(Spells spell)
    {
        this.spell = spell;
        iconImage.sprite = spell.Icon;  // 设置图标
        tooltipText.text = spell.GetDescription();  // 设置 Tooltip 文本
        NameText.text = spell.Name;  // 设置 Buff 名称

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tooltip != null)
        {
            tooltip.SetActive(true);  // 显示 Tooltip
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tooltip != null)
        {
            tooltip.SetActive(false);  // 隐藏 Tooltip
        }
    }
}
