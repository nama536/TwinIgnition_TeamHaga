using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 moveDirection;

    // 発射台から方向を設定してもらうためのメソッド
    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }

    void Update()
    {
        // 指定された方向に直進
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    // 画面外に出たら消える（簡易版）
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}