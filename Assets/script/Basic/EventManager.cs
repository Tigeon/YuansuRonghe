using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventManager : MonoBehaviour
{
    [SerializeField] private List<RandomEvent> StaticEvents;
    // 保存游戏的所有事件
    public static List<RandomEvent> staticEvents { get; private set; }

    // 当前事件
    private RandomEvent currentEvent;
    // 当前事件文本索引
    private int currentEventTextIndex;

    //优先队列
    private List<RandomEvent> priorityEvents = new List<RandomEvent>();
    //普通队列
    private List<RandomEvent> normalEvents = new List<RandomEvent>();

    public GameObject EventUIObject;
    public GameObject BackGround;
    public GameObject EventImage;
    public TextMeshProUGUI TitleText;
    public TextMeshProUGUI EventText;
    
    public GameObject[] choicesObjects;

    public static EventManager Instance { get; private set; }

    private void Awake()
    {
        // Ensure only one instance of EventManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        staticEvents = StaticEvents;

        foreach(var Event in staticEvents){
            if (Event.normal)
            {
                normalEvents.Add(Event);
            }
        }
        HideEventUI();
    }

    // 添加新的事件到事件列表
    public void AddNormalEvent(int randomEventID)
    {
        normalEvents.Add(staticEvents[randomEventID]);
    }

    // 移除事件
    public void RemoveNormalEvent(int randomEventID)
    {
        normalEvents.RemoveAll(e => e.id == randomEventID);
    }

    // 添加新的事件到优先事件列表
    public void AddPriorityEvent(int randomEventID)
    {
        priorityEvents.Add(staticEvents[randomEventID]);
    }

    // 移除优先事件
    public void RemovePriorityEvent(int randomEventID)
    {
        priorityEvents.RemoveAll(e => e.id == randomEventID);
    }

    // 随机触发一个事件，如果有优先事件，50%优先触发优先事件
    public void TriggerRandomEvent()
    {
        currentEvent = ChooseEvent();
        currentEventTextIndex = 0;
        ShowEventUI(currentEvent);
    }

    // 根据玩家的选择来更新事件状态
    public void TriggerChoice(int choiceIndex)
    {
        // 根据选择的选项，触发对应的事件
        RandomEvent.Connection chosenConnection = currentEvent.eventTexts[currentEventTextIndex].connections[choiceIndex];
        // 执行选择结果的效果
        ExecuteEffect(chosenConnection.choice.effect);
        // 根据连接更新事件文本索引
        currentEventTextIndex = chosenConnection.destinationID;
        if(currentEventTextIndex != -1){
            // 显示更新后的事件UI
            ShowEventUI(currentEvent);
        }else{
            // 隐藏事件UI
            HideEventUI();
            // 恢复玩家控制
            //PlayerController.Instance.enabled = true;
            NodeMove.Instance.StartMoving();
        }
        
    }

    // 执行效果
    private void ExecuteEffect(RandomEvent.Choice.Effect effect)
    {
        // 在这里你可以根据effect的内容来更新玩家的状态
        // 比如你可以在这里修改玩家的生命值和金币
    }

    // 选择一个事件，可以根据您的规则进行修改
    private RandomEvent ChooseEvent()
    {
        //50%的概率触发优先事件
        if (priorityEvents.Count > 0 && Random.Range(0, 2) == 0)
        {
            return priorityEvents[Random.Range(0, priorityEvents.Count)];
        }
        else
        {
            return normalEvents[Random.Range(0, normalEvents.Count)];
        }
    }

    // 显示事件UI
    public void ShowEventUI(RandomEvent chosenEvent)
    {
        EventImage.GetComponent<Image>().sprite = chosenEvent.eventImage;
        TitleText.text = chosenEvent.eventTitle;
        EventText.text = chosenEvent.eventTexts[currentEventTextIndex].text;        
        EventUIObject.SetActive(true);
        //根据事件的颜色改变背景颜色
        BackGround.GetComponent<Image>().color = chosenEvent.color;
        Debug.Log(chosenEvent.eventTexts[currentEventTextIndex].connections.Length);
        //根据选择的数量显示选择按钮
        for(int i = 0; i < choicesObjects.Length; i++)
        {
            if (i < chosenEvent.eventTexts[currentEventTextIndex].connections.Length)
            {
                
                choicesObjects[i].SetActive(true);
                choicesObjects[i].GetComponentInChildren<TextMeshProUGUI>().text = chosenEvent.eventTexts[currentEventTextIndex].connections[i].choice.text;
            }
            else
            {
                choicesObjects[i].SetActive(false);
            }
        }
    }

    // 隐藏事件UI
    public void HideEventUI()
    {
        EventUIObject.SetActive(false);
    }
}
