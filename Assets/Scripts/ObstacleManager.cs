using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [Header("障害物")]
    public GameObject obstaclePrefab;
    [Header("次の障害物がスポンするまでの間隔")] 
    public float spawnInterval = 1.0f; 
    public float minX = -8f;   // 画面の左端
    public float maxX = 8f;    // 画面の右端
    public float spawnY = 6f;  // 画面の上端（生成位置）
    public float destroyY = -6f; // 画面の下端（消去位置）

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        // インスペクターで設定した時間を超えたら生成
        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0f;
        }
    }

    void SpawnObstacle()
    {
        // ランダムな横位置を決める
        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(randomX, spawnY, 0);

        // 障害物を生成
        GameObject newObstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);

        // 【自動処理】画面外に落ちたら勝手に消えるコンポーネントをその場で追加
        Destroyer2D destroyer = newObstacle.AddComponent<Destroyer2D>();
        destroyer.limitY = destroyY;
    }
}

// 画面外で自動削除するためだけのミニクラス
public class Destroyer2D : MonoBehaviour
{
    public float limitY;
    void Update()
    {
        if (transform.position.y < limitY)
        {
            Destroy(gameObject);
        }
    }
}
