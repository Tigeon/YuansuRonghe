using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance { get; private set; }

    [SerializeField] private Canvas battleCanvas;
    [SerializeField] private Canvas eventCanvas;
    [SerializeField] private Canvas mapCanvas;
    [SerializeField] private Canvas TopCanvas;

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

    public void ShowBattleCanvas()
    {
        battleCanvas.enabled = true;
        eventCanvas.enabled = false;
        mapCanvas.enabled = false;
    }

    public void ShowEventCanvas()
    {
        battleCanvas.enabled = false;
        eventCanvas.enabled = true;
        mapCanvas.enabled = false;
    }

    public void ShowMapCanvas()
    {
        battleCanvas.enabled = false;
        eventCanvas.enabled = false;
        mapCanvas.enabled = true;
    }

    // Optionally, a method to hide all canvases
    public void HideAllCanvases()
    {
        battleCanvas.enabled = false;
        eventCanvas.enabled = false;
        mapCanvas.enabled = false;
    }
}
