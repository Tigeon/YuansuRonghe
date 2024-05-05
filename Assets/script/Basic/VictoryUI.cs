using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryUI : MonoBehaviour
{
    public RectTransform VictoryUIObject;
    public RectTransform RewardUIObject;
    public GameObject NextStageButton;
    public GameObject Text1;
    public GameObject Text2;
    public GameObject Text3;

    // Wait time before showing the victory UI
    public const float ShowVictoryUIDelay = 1f;
    public const float ShowNextStageUIDelay = .7f;

    public static VictoryUI Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        HideAllUI();
    }

    public void Victory()
    {
        StartCoroutine(ShowVictoryUI());
    }

    IEnumerator ShowVictoryUI()
    {
        yield return new WaitForSeconds(ShowVictoryUIDelay);
        VictoryUIObject.gameObject.SetActive(true);
        yield return new WaitForSeconds(ShowNextStageUIDelay);
        Text1.SetActive(true);
        yield return new WaitForSeconds(ShowNextStageUIDelay);
        Text2.SetActive(true);
        yield return new WaitForSeconds(ShowNextStageUIDelay);
        RewardUIObject.gameObject.SetActive(true);
        Text3.SetActive(true);
        yield return new WaitForSeconds(ShowNextStageUIDelay);
        NextStageButton.gameObject.SetActive(true);
    }

    public void NextStage()
    {
        HideAllUI();
        NodeGenerator.Instance.ShowMap();
        NodeMove.Instance.StartMoving();
    }

    public void HideAllUI()
    {
        VictoryUIObject.gameObject.SetActive(false);
        RewardUIObject.gameObject.SetActive(false);
        NextStageButton.gameObject.SetActive(false);
        Text1.SetActive(false);
        Text2.SetActive(false);
        Text3.SetActive(false);
    }
}
