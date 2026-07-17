using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("ボス設定")]
    [SerializeField] private GameObject bossPrefab;

    // [Header("出現時間（秒）")]
    // [SerializeField] private float spawnTime = 30f;

    [Header("出現位置")]
    [SerializeField] private Vector2 spawnPosition = new Vector2(0f, 4f);

    [Header("マネージャー")]
    [SerializeField] private ObstacleManager obstacleManager;
    [SerializeField] private StageProgressBar stageProgressBar;
    [SerializeField] private BackGroundMover backGroundMover;

    // private float timer = 0f;
    // private bool bossSpawned = false;

    // void Update()
    // {
    //     // 既にボスを出していたら何もしない
    //     if (bossSpawned)
    //         return;

    //     timer += Time.deltaTime;

    //     if (timer >= spawnTime)
    //     {
    //         SpawnBoss();
    //     }
    // }

    public void SpawnBoss()
    {
        // ボス生成
        GameObject boss = Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
        // 設定
        BossHealth bh = boss.GetComponent<BossHealth>();
        bh.EnemyManager = this;
        backGroundMover.m_NowSpeed = Vector2.zero;

        // bossSpawned = true;

        // 障害物の生成を停止
        if (obstacleManager != null)
        {
            obstacleManager.enabled = false;
        }
    }

    // ボス撃破
    public void BossClear()
    {
        stageProgressBar.Arrived = false;
        backGroundMover.Restart();
        MaingameManager.Instance.DoClear = true;
        MaingameManager.Instance.Score += 1000;
        StartCoroutine(MaingameManager.Instance.GameEnd());
    }
}