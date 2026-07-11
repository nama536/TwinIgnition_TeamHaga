using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("ボス設定")]
    [SerializeField] private GameObject bossPrefab;

    [Header("出現時間（秒）")]
    [SerializeField] private float spawnTime = 30f;

    [Header("出現位置")]
    [SerializeField] private Vector2 spawnPosition = new Vector2(0f, 4f);

    [Header("障害物マネージャー")]
    [SerializeField] private ObstacleManager obstacleManager;

    private float timer = 0f;
    private bool bossSpawned = false;

    void Update()
    {
        // 既にボスを出していたら何もしない
        if (bossSpawned)
            return;

        timer += Time.deltaTime;

        if (timer >= spawnTime)
        {
            SpawnBoss();
        }
    }

    void SpawnBoss()
    {
        // ボス生成
        Instantiate(bossPrefab, spawnPosition, Quaternion.identity);

        bossSpawned = true;

        // 障害物の生成を停止
        if (obstacleManager != null)
        {
            obstacleManager.enabled = false;
        }
    }
}