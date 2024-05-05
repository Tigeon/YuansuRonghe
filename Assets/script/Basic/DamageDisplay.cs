using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamageDisplay : MonoBehaviour
{
    public TextMeshProUGUI damagePrefab;
    public Canvas targetCanvas;
    public Transform ParentTransform;
    public float displayDuration = 1f;
    public Vector3 offset = new Vector3(0, 2, 0);  // Slightly raised above the target
    public float damageRiseSpeed = 1f;

    public static DamageDisplay Instance { get; private set; }

    private Queue<TextMeshProUGUI> damagePool = new Queue<TextMeshProUGUI>();
    private const int poolSize = 3; // Adjust size as needed

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializePool();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            TextMeshProUGUI instance = Instantiate(damagePrefab, ParentTransform);
            instance.gameObject.SetActive(false);
            damagePool.Enqueue(instance);
        }
    }

    public void ShowDamage(int damage, Transform target, Color color)
    {
        if (damagePool.Count > 0)
        {
            TextMeshProUGUI damageInstance = damagePool.Dequeue();
            damageInstance.gameObject.SetActive(true);
            damageInstance.text = damage.ToString();
            damageInstance.color = color;

            // Start the coroutine to update the damage display
            StartCoroutine(UpdateDamageDisplay(damageInstance, target));
            StartCoroutine(DestroyDamageDisplay(damageInstance, displayDuration));
        }
    }


    private IEnumerator UpdateDamageDisplay(TextMeshProUGUI damageInstance, Transform target)
    {
        float timer = 0;
        Vector3 startPosition = target.position + offset;

        while (damageInstance != null && target != null && timer < displayDuration)
        {
            damageInstance.transform.position = Vector3.Lerp(startPosition, startPosition + new Vector3(0, damageRiseSpeed, 0), timer / displayDuration);
            timer += Time.deltaTime;
            yield return null;
        }
    }


    private void UpdateDamagePosition(TextMeshProUGUI damageInstance, Vector3 targetPosition)
    {
        if (damageInstance != null)
        {
            damageInstance.transform.position = targetPosition + offset;
        }
    }


    private IEnumerator DestroyDamageDisplay(TextMeshProUGUI damageInstance, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (damageInstance != null)
        {
            damageInstance.gameObject.SetActive(false);
            damagePool.Enqueue(damageInstance);
        }
    }

}
