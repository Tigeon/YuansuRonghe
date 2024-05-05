using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;  // 需要引入用于处理UI事件的命名空间
using TMPro;

public class BuffIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public SpriteRenderer iconImage;
    public GameObject tooltip;  // Tooltip UI 对象
    public TextMeshProUGUI tooltipText;  // Tooltip 文本对象
    public TextMeshProUGUI BuffStackText;  // 显示 Buff 堆叠数的文本对象
    public Buff buff;  // 关联的 Buff 对象

    void Start()
    {
        if (tooltip != null)
        {
            tooltip.SetActive(false);  // 初始时隐藏 Tooltip
        }
    }

    public void Initialize(Buff buff)
    {
        this.buff = buff;
        iconImage.sprite = buff.GetImage();  // 设置图标
        tooltipText.text = buff.GetInfo();  // 设置 Tooltip 文本
        UpdateStacksDisplay();
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

    public void UpdateStacksDisplay()
    {
        BuffStackText.text = buff.Stacks.ToString();  // 更新显示的 Buff 堆叠数
        if (buff.Stacks <= 1)
        {
            BuffStackText.gameObject.SetActive(false);  // 如果堆叠数为 1 或以下，隐藏显示
        }
        else
        {
            BuffStackText.gameObject.SetActive(true);  // 否则显示
        }
    }
}
