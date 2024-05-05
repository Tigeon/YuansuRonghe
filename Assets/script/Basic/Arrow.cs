using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 导航箭头
/// </summary>
public class Arrow : MonoBehaviour
{
    public static Arrow Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private Vector3 startPoint;
    public RawImage arrowImage;

    void Update()
    {
        UpdateArrow();
    }

    //更新箭头
    public void UpdateArrow()
    {        
        Vector3 currentMousePosition = GetMousePosition();
        Vector3 direction = currentMousePosition - startPoint;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);

        float distance = direction.magnitude;
        transform.localScale = new Vector3(1f, distance/100f, 1f);

        // 更新箭头位置，使其保持在最初的位置和鼠标位置的中间
        transform.position = startPoint + direction * 0.5f;
    }

    public void disableArrow(){
        arrowImage.enabled = false;
    }

    public void activeArrow(){
        Debug.Log("activeArrow");
        arrowImage.enabled = true;
        transform.localScale = Vector3.zero;
        startPoint = GetMousePosition();
        transform.position = startPoint;
    }

    private Vector3 GetMousePosition()
    {
        return Input.mousePosition;
    }
}

