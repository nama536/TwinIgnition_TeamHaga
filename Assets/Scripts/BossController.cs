using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("登場")]
    [SerializeField] private float enterSpeed = 3f;
    [SerializeField] private float stopY = 3.5f;


    [Header("左右移動")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float moveRange = 3f;


    [Header("円形弾幕")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int bulletCount = 16;
    [SerializeField] private float bulletInterval = 3f;


    [Header("レーザー")]
    [SerializeField] private GameObject laserPrefab;

    [SerializeField] private Transform leftLaserPoint;
    [SerializeField] private Transform rightLaserPoint;

    [SerializeField] private float laserInterval = 8f;

    //レーザー角度
    [SerializeField] private float leftLaserAngle = -45f;
    [SerializeField] private float rightLaserAngle = -135f;


    private bool entered = false;

    private Vector3 centerPosition;

    private float bulletTimer;
    private float laserTimer;



    void Update()
    {
        if (!entered)
        {
            EnterStage();
            return;
        }


        MoveSide();


        bulletTimer += Time.deltaTime;
        laserTimer += Time.deltaTime;



        if (bulletTimer >= bulletInterval)
        {
            ShootCircle();
            bulletTimer = 0f;
        }



        if (laserTimer >= laserInterval)
        {
            ShootLaser();
            laserTimer = 0f;
        }
    }

    // 登場
    void EnterStage()
    {
        transform.position += Vector3.down * enterSpeed * Time.deltaTime;

        if(transform.position.y <= stopY)
        {
            transform.position = new Vector3(transform.position.x, stopY, transform.position.z);

            centerPosition = transform.position;

            entered = true;
        }
    }

    // 左右移動
    void MoveSide()
    {
        float x = Mathf.Sin(Time.time * moveSpeed) * moveRange;

        transform.position = new Vector3(centerPosition.x + x, stopY, centerPosition.z);
    }

    // 円形弾幕
    void ShootCircle()
    {
        if(bulletPrefab == null)
            return;


        float angleStep = 360f / bulletCount;



        for(int i = 0; i < bulletCount; i++)
        {
            float angle = angleStep * i;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            EnemyBullet enemy = bullet.GetComponent<EnemyBullet>();
            if(enemy != null)
            {
                enemy.SetDirection(angle);
            }
        }

        SoundManager.Instance.Play("Shot");
    }

    // 左右レーザー
    void ShootLaser()
    {
        if(laserPrefab == null)
            return;



        //左レーザー
        if(leftLaserPoint != null)
        {
            Instantiate(laserPrefab, leftLaserPoint.position, Quaternion.Euler(0, 0, leftLaserAngle));
        }



        //右レーザー
        if(rightLaserPoint != null)
        {
            Instantiate(laserPrefab, rightLaserPoint.position, Quaternion.Euler(0, 0, rightLaserAngle));
        }

        SoundManager.Instance.Play("Shot");
    }
}