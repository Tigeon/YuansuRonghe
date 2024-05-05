using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;  // 需要引入用于处理UI事件的命名空间
using TMPro;

public class ActionIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public SpriteRenderer iconImage;
    public GameObject tooltip;  // Tooltip UI 对象
    public TextMeshProUGUI tooltipText;  // Tooltip 文本对象
    public UnitAction Action;  // 关联的 Buff 对象

    void Start()
    {
        if (tooltip != null)
        {
            tooltip.SetActive(false);  // 初始时隐藏 Tooltip
        }
    }

    public void UpdateUI(UnitAction action)
    {
        this.Action = action;
        tooltipText.text = action.ActionDescription;  // 设置 Tooltip 文本
        iconImage.sprite = action.Icon;  // 设置图标
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
