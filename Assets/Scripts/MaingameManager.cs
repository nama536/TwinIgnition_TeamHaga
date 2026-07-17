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
    //　クリアしたか
    [HideInInspector] public bool DoClear = false;
    //　スコア
    [HideInInspector] public int Score = 0;

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

    //　リザルトへ移動
    public IEnumerator GameEnd()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("ResultScene");
    }
}
