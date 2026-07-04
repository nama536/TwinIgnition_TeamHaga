using UnityEngine;
using System.Collections;

public class SetBarrageSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab; // 弾のプレハブ
    [SerializeField] private int bulletCount = 16;    // 1パターンあたりの弾数（等間隔に配置）
    [SerializeField] private float fireRate = 3.0f;   // セットごとの発射間隔（秒）

    [Header("弾幕の速度設定")]
    [SerializeField] private float lowSpeed = 3f;
    [SerializeField] private float highSpeed = 7f;

    [Header("自機外しのズレ角度（度数）")]
    [SerializeField] private float angleOffset = 15f; // 自機外しでどれくらいずらすか

    [Header("パターン間の発射間隔（秒）")]
    public float delayBetweenPatterns = 0.2f; // ★1〜4の間の時間差

    private Transform playerTransform;
    private float timer;

    void Start()
    {
        // "Player" タグがついたゲームオブジェクトを探す
        GameObject player = GameObject.FindWithTag("Player");
        
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogWarning("シーン内に 'Player' タグがついたオブジェクトが見つかりません！");
        }
    }

    void Update()
    {
        if (playerTransform == null) return;

        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            // ★コルーチンを起動する
            StartCoroutine(FireComboBarrageRoutine());
            timer = 0f;
        }
    }

    IEnumerator FireComboBarrageRoutine()
    {
        // 4つのパターンの設定を配列にする
        // (速度, ズレ角度)
        (float speed, float offset)[] patterns = new (float, float)[]
        {
            (lowSpeed, angleOffset),   // 1: 低速・自機外し
            (highSpeed, 0f),           // 2: 高速・自機狙い
            (lowSpeed, 0f),            // 3: 低速・自機狙い
            (highSpeed, -angleOffset)  // 4: 高速・自機外し
        };

        // パターンを1つずつ順番に処理
        foreach (var p in patterns)
        {
            // 発射する瞬間の自機の方向を計算（その都度計算するので、動きに追従します）
            Vector2 targetDirection = (playerTransform.position - transform.position).normalized;
            float baseAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

            float patternBaseAngle = baseAngle + p.offset;
            float angleStep = 360f / bulletCount;

            // 1パターン分（全方位）の弾を生成
            for (int i = 0; i < bulletCount; i++)
            {
                float finalAngle = patternBaseAngle + (i * angleStep);
                float dirX = Mathf.Cos(finalAngle * Mathf.Deg2Rad);
                float dirY = Mathf.Sin(finalAngle * Mathf.Deg2Rad);
                Vector2 moveDir = new Vector2(dirX, dirY);

                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                Bullet bulletScript = bullet.GetComponent<Bullet>();
                
                if (bulletScript != null)
                {
                    bulletScript.speed = p.speed;
                    bulletScript.SetDirection(moveDir);
                }
            }

            // ★指定した秒数だけ処理を中断し、次のフレーム以降に持ち越す
            yield return new WaitForSeconds(delayBetweenPatterns);
        }
    }
}