using UnityEngine;

public class ObstacleHealth : MonoBehaviour
{
    [Header("耐久度設定")]
    public int maxHp = 3; 
    private int currentHp;

    [Header("アイテムドロップ設定")]
    public float dropChance = 0.5f; // 例: 0.5なら50%の確率


    public GameObject[] itemPrefabs; 

    void Start()
    {
        currentHp = maxHp;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            currentHp--;

            // 当たった弾を消す
            Destroy(collision.gameObject);

            // HPが0になったら
            if (currentHp <= 0)
            {
                // アイテム抽選の処理を実行
                TryDropItem();

                // 自分自身を破壊
                Destroy(gameObject);
            }
        }
    }

    void TryDropItem()
    {
        // アイテムリストが空っぽなら何もしない
        if (itemPrefabs == null || itemPrefabs.Length == 0) return;

        float randomRoll = Random.value;

        // 運良く確率をクリアしたらアイテムを生成
        if (randomRoll <= dropChance)
        {
            int randomIndex = Random.Range(0, itemPrefabs.Length);
            GameObject selectedItem = itemPrefabs[randomIndex];

            // 障害物と同じ位置にアイテムを生成
            if (selectedItem != null)
            {
                Instantiate(selectedItem, transform.position, Quaternion.identity);
            }
        }
    }
}
