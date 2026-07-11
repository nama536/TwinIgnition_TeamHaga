using UnityEngine;

public class Laser : MonoBehaviour
{
    [Header("移動速度")]
    [SerializeField] private float speed = 10f;

    [Header("生存時間")]
    [SerializeField] private float lifeTime = 3f;

    [Header("ダメージ")]
    [SerializeField] private int damage = 1;

    void Start()
    {
        // 一定時間後に削除
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // 下方向へ移動
        transform.Translate(Vector2.down * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // プレイヤーに当たったら
        if (collision.CompareTag("Player"))
        {
            // プレイヤー側にHPスクリプトがあるなら呼び出す
            // collision.GetComponent<PlayerHealth>()?.TakeDamage(damage);

            Destroy(gameObject);
        }

        // 壁に当たったら消える
        if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}