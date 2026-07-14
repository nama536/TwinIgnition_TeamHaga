using UnityEngine;

public class MaingameManager : MonoBehaviour
{
    public static MaingameManager Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    //　現在ゲーム中かどうか
    [HideInInspector] public bool IsGaming = false;
}
