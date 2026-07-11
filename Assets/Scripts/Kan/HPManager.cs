using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    [SerializeField] private GameObject HP1;
    [SerializeField] private GameObject HP2;
    [SerializeField] private GameObject HP3;
   
    private void OnCollisionEnter(Collision collision)
    {
        HP3.SetActive(false);
        if (HP3.activeSelf == false)
        {
            HP2.SetActive(false);
        }
        if (HP2.activeSelf == false)
        {
            HP1.SetActive(false);
        }
        if (HP1.activeSelf == false)
        {
            Debug.Log("Game Over");
        }
    }
}

