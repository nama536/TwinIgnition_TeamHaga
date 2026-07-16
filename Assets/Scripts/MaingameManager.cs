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
    //　弾を撃てるか
    [HideInInspector] public bool CanShot = true;

    [SerializeField] HPManager hpManager;
    [SerializeField] EnergyManager energyManager;

    // プレイヤーへのダメージ処理
    public void GetDamage()
    {
        hpManager.GetDamage();
    }

    //　弾エネルギー消費処理
    public void DoShot()
    {
        energyManager.ConsumeEnergyForShoot();
    }
}
