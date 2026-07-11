using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Fragment : MonoBehaviour
{
    [SerializeField] private GameObject fragmentPrefab;
    [SerializeField] public int fragmentCount = 8;
    [SerializeField] public float explosionForce = 0.1f;
    [SerializeField] private float destroyTime = 2f;
    [SerializeField] private float radius = 0.1f;

    public void Break()
    {
        for (int i = 0; i < fragmentCount; i++)
        {
            GameObject fragment = Instantiate(
                fragmentPrefab,
                transform.position,
                Quaternion.identity);

            Rigidbody2D rb = fragment.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                Vector2 center = transform.position; // 爆発中心

                // 生成位置からランダム方向へ飛ばす
                Vector2 direction = Random.insideUnitCircle.normalized;

                // 距離による減衰（生成直後は距離0なので最大）
                float distance = Vector2.Distance(rb.position, center);
                float strength = Mathf.Clamp01(1f - distance / radius);

                rb.AddForce(
                    direction * explosionForce * strength,
                    ForceMode2D.Force);
            }
            Destroy(fragment, 2f);
        }
        Destroy(gameObject);
    }
}