using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform target;  // 目标位置
    public float speed = 10f; // 飞射物速度
    public GameObject explosionEffectPrefab;  // 爆炸特效的预制体

    void Update()
    {
        if (target != null)
        {
            //自身保持旋转
            transform.rotation = Quaternion.Euler(0, 0, 10f * Time.deltaTime);

            // 向目标移动
            Vector3 direction = target.position - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;

            if (direction.magnitude <= distanceThisFrame)
            {
                HitTarget();
                return;
            }

            transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void HitTarget()
    {
        // 实例化爆炸特效
        if (explosionEffectPrefab != null)
        {
            GameObject effect = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 2f);  // 假设特效2秒后自动销毁
        }

        Destroy(gameObject);  // 销毁飞射物
    }

    //在死亡时发动Spellbook的SpellCast方法
    private void OnDestroy()
    {
        SpellBook.Instance.SpellCastToCurrentTarget();
    }
}
