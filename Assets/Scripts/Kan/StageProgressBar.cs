using UnityEngine;

public class StageProgressBar : MonoBehaviour
{
    public float speed = 0.1f; // Speed of the progress bar
    [SerializeField] private GameObject rocket;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rocket.transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
