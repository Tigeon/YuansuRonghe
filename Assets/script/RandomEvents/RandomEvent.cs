using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Random Event")]
public class RandomEvent : ScriptableObject
{
    public int id; // 事件ID，用于识别事件
    public string eventTitle; // 事件的标题
    public Color color; // 事件的颜色
    public bool normal; // 是否是普通事件
    public Sprite eventImage; // 事件中的图片
    public EventText[] eventTexts; // 存储更多的文本
    public bool isRepeatable; // 是否可以重复
    

    [System.Serializable]
    public class EventText
    {
        [TextArea(7,7)] public string text; // 文本内容
        public Connection[] connections; // 从这个文本出去的连接
    }

    [System.Serializable]
    public class Connection
    {
        public int destinationID; // 连接指向的文本ID
        public Choice choice; // 做出这个选择的结果
    }

    [System.Serializable]
    public class Choice
    {
        [TextArea(7,3)] public string text;
        public bool isSelectable = true; // 开关，表示该选择是否可以被选择
        public int requiredItemID; // 如果有必要的道具，可以选择这个选项
        public Effect effect;

        [System.Serializable]
        public class Effect
        {
            public int healthChange;
            public int goldChange;
        }
        
        // 你可以调用这个方法来检查玩家是否有需要的物品，使选择变得可能
        public void CheckRequirements()
        {
            if (requiredItemID == -1)
            {
                isSelectable = true;
            }
            else
            {
                isSelectable = Inventory.Instance.HasItem(requiredItemID);
            }
        }
    }
}
