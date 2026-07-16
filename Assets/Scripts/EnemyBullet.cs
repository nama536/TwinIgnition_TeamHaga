using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Header("弾の速度")]
    [SerializeField] private float speed = 5f;

    [Header("画面外で消えるまでの時間")]
    [SerializeField] private float lifeTime = 8f;

    private Vector2 direction;

    void Start()
    {
        // 一定時間後に自動削除
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    // BossControllerから呼ばれる
    public void SetDirection(float angle)
    {
        float rad = angle * Mathf.Deg2Rad;

        direction = new Vector2(
            Mathf.Cos(rad),
            Mathf.Sin(rad)
        ).normalized;

        // 弾の向きを進行方向に合わせる（任意）
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // プレイヤーに当たったら消える
        if (collision.CompareTag("Player"))
        {
            // プレイヤー側へのダメージ処理
            MaingameManager.Instance.GetDamage();

            Destroy(gameObject);
        }
    }
}