using UnityEngine;
using UnityEngine.UI;

public class BGManager : MonoBehaviour
{
    [SerializeField] private GameObject Bg1;
    [SerializeField] private GameObject Bg2;
    [SerializeField] private StageProgressBar stageProgressBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Bg1.GetComponent<Image>().material.mainTextureOffset.y > 0.99f)
        {
            Bg1.SetActive(false);
        }
        if (stageProgressBar.Arrived)
        {
            Bg2.SetActive(false);
        }
    }
}
