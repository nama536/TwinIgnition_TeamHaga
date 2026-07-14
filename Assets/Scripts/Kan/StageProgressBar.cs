using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class StageProgressBar : MonoBehaviour
{
    public float speed = 0.1f; // Speed of the progress bar
    [SerializeField] private GameObject rocket;
    [SerializeField] private GameObject target;
    public bool Arrived = false;
    private bool doBoss = false; // ボスに到達済みか

    [SerializeField] private EnemyManager enemyManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Arrived) return;

        rocket.transform.position = Vector3.MoveTowards(
            rocket.transform.position,
            target.transform.position,
            speed * Time.deltaTime);

        if (target.transform.position.y - rocket.transform.position.y < 0.01f)
        {
            Arrived = true;
            Debug.Log("�����I");
        }

        if (target.transform.position.y - rocket.transform.position.y < 1f && doBoss == false)
        {
            Arrived = true;
            doBoss = true;
            Debug.Log("Boss");
            // ボス召喚
            enemyManager.SpawnBoss();
        }
    }
}
