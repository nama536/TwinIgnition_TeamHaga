using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class StageProgressBar : MonoBehaviour
{
    public float speed = 0.1f; // Speed of the progress bar
    [SerializeField] private GameObject rocket;
    [SerializeField] private GameObject target;
    private bool arrived = false;
    private bool ifBoss = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (arrived) return;

        rocket.transform.position = Vector3.MoveTowards(
            rocket.transform.position,
            target.transform.position,
            speed * Time.deltaTime);

        if (target.transform.position.y - rocket.transform.position.y < 0.01f)
        {
            arrived = true;
            Debug.Log("“ž’…I");
        }

        if (!ifBoss && target.transform.position.y - rocket.transform.position.y < 1f)
        {
            arrived = true;
            Debug.Log("Boss");
        }
    }
}
