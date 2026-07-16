using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] float _selfdestructTimer = 2.0f;

    // Update is called once per frame
    void Update()
    {
        _selfdestructTimer -= Time.deltaTime;
        if (_selfdestructTimer < 0f){
            Destroy(this.gameObject);
        }
    }
}
