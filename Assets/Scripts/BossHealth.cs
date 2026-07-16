using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [Header("ボスHP")]
    [SerializeField] private int maxHp = 100;

    private int currentHp;

    [Header("撃破エフェクト（任意）")]
    [SerializeField] private GameObject destroyEffect;

    [HideInInspector] public EnemyManager EnemyManager;

    void Start()
    {
        currentHp = maxHp;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet"))
            return;

        // プレイヤーの弾を消す
        Destroy(collision.gameObject);

        // ダメージ
        currentHp--;

        // 撃破判定
        if (currentHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // 爆発エフェクト
        if (destroyEffect != null)
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
        }
        SoundManager.Instance.Play("Dokann");

        // ボスを破壊
        EnemyManager.BossClear();
        Destroy(gameObject);
    }

    // 他のスクリプトからダメージを与えたい時用
    public void TakeDamage(int damage)
    {
        currentHp -= damage;

        if (currentHp <= 0)
        {
            Die();
        }
    }

    // HP取得（UI表示用）
    public int GetCurrentHp()
    {
        return currentHp;
    }

    public int GetMaxHp()
    {
        return maxHp;
    }
}