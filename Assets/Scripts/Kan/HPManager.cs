using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    [SerializeField] private GameObject HP1;
    [SerializeField] private GameObject HP2;
    [SerializeField] private GameObject HP3;
   
    public void GetDamage()
    {
        if (HP2.activeSelf == false)
        {
            HP1.SetActive(false);
            Debug.Log("Game Over");
            MaingameManager.Instance.IsGaming = false;
            StartCoroutine(MaingameManager.Instance.GameEnd());
        }
        else if (HP3.activeSelf == false)
        {
            HP2.SetActive(false);
        }
        else
        {
            HP3.SetActive(false);
        }

        SoundManager.Instance.Play("Damage");
    }
}

