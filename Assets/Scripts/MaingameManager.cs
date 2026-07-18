using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MaingameManager : MonoBehaviour
{
    public static MaingameManager Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    //　現在ゲーム中かどうか
    [HideInInspector] public bool IsGaming = false;
    //　弾を撃てるか
    [HideInInspector] public bool CanShot = true;

    //　ゲームデータ保存用
    public GameData GameData;

    [SerializeField] HPManager hpManager;
    [SerializeField] EnergyManager energyManager;

    void Start()
    {
        GameData.DoClear = false;
        GameData.Score = 0;
    }

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

    //　リザルトへ移動
    public IEnumerator GameEnd()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("ResultScene");
    }
}
