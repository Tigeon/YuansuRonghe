using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Item")]
public class Item : ScriptableObject
{
    public int Id; // 物品的ID
    public string itemName;
    public Sprite itemSprite; // 物品的图像
    public string description; // 物品的描述
    public bool isConsumable; // 物品是否可消耗
    public bool isStackable; // 物品是否可堆叠


    public void ApplyEffect()
    {

    }
}
