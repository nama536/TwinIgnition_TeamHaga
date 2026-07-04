using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("障害物")]
    public GameObject BossPrefab;
    [Header("次の障害物がスポンするまでの間隔")] 
    public float spawnTime = 1.0f; 
    public float spawnY = 6f;   // 画面の上端（生成位置）
    public float spawnX = 2.5f; // 画面真ん中（生成位置）

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        // インスペクターで設定した時間を超えたら生成
        if (timer >= spawnTime)
        {
            SpawnBoss();
        }
    }

    void SpawnBoss()
    {
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);

        // 障害物を生成
        GameObject newBoss = Instantiate(BossPrefab, spawnPosition, Quaternion.identity);
    }
}
