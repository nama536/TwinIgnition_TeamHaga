using Cysharp.Threading.Tasks;
using LitMotion;
using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum FadeType
{
    Normal,
    Iris,
    Spiral,
}

[Serializable]
public class FadeData
{
    public FadeType Type;
    public Material FadeMat;
}
public class FadeManager : MonoBehaviour
{
    private static FadeManager _instance;
    public static FadeManager Instance => _instance;

    float Speed = 1.0f;            //フェードするスピード
    float red, green, blue, alfa;
    bool _isLoading = false;        //二重ロード防止
    private SemaphoreSlim semaphore = new SemaphoreSlim(1);   //フェードが同時実行されないようにするロック

    [SerializeField] Image _fadeImage;
    [SerializeField] private List<FadeData> _fadeData = new List<FadeData>();
    private FadeType _currentType;

    // 追加
    [SerializeField] private float _fadeTime = 0.0f;
    private int _shaderProgressID = Shader.PropertyToID("_Progress");

    private CancellationTokenSource _cts = new CancellationTokenSource();
    private CancellationToken _token;


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        red = _fadeImage.color.r;
        green = _fadeImage.color.g;
        blue = _fadeImage.color.b;
        alfa = _fadeImage.color.a;

        _token = _cts.Token;

        FadeIn().Forget();
    }


    // UnityのButtonコンポーネントから直接呼ぶためのラッパーメソッド
    public void TriggerFadeAndLoadScene(string sceneName)
    {
        SoundManager.Instance.Play("Click");
        // UniTaskVoidのメソッドを安全に呼び出す（Forgetで警告を消す）
        FadeAndLoadScene(sceneName).Forget();
    }



    public async UniTaskVoid FadeAndLoadScene(string sceneName)
    {
        if (_isLoading) return;

        _isLoading = true;

        await FadeOut();
        await SceneManager.LoadSceneAsync(sceneName);
        await FadeIn();

        _isLoading = false;
    }

    [Button]
    //public async UniTask FadeIn(FadeType type = FadeType.Iris)
    //{
    //    await semaphore.WaitAsync();
    //    var token = destroyCancellationToken;

    //    try
    //    {
    //        SetFadeMat(type);
    //        _fadeImage.gameObject.SetActive(true);
    //        await LMotion.Create(1f, 0f, _fadeTime)
    //                     .Bind(p => SetMatProgress(p))
    //                     .AddTo(_fadeImage.gameObject)
    //                     .ToUniTask(token);
    //        _fadeImage.gameObject.SetActive(false);
    //    }
    //    finally
    //    {
    //        semaphore.Release();
    //    }
    //}

    //[Button]
    //public async UniTask FadeOut(FadeType type = FadeType.Iris)
    //{
    //    // 他のフェードが動いている場合は待機
    //    await semaphore.WaitAsync();
    //    var token = destroyCancellationToken;

    //    try
    //    {
    //        SetFadeMat(type);
    //        _fadeImage.gameObject.SetActive(true);
    //        await LMotion.Create(0, 1f, _fadeTime)
    //                     .Bind(p => SetMatProgress(p))
    //                     .AddTo(_fadeImage.gameObject)
    //                     .ToUniTask(token);
    //    }
    //    finally
    //    {
    //        // ロック状態を解除
    //        semaphore.Release();
    //    }
    //}

    /// <summary>
    /// Progressパラメーターに値を入れる
    /// </summary>
    /// <param name="progress"></param>
    private void SetMatProgress(float progress)
    {
        _fadeImage.material.SetFloat(_shaderProgressID, progress);
    }

    //private void SetFadeMat(FadeType type)
    //{
    //    if (type == _currentType) return;
    //    foreach (var data in _fadeData)
    //    {
    //        if (type == data.Type)
    //        {
    //            _fadeImage.material = data.FadeMat;
    //            _currentType = type;
    //            return;
    //        }
    //    }
    //    Debug.LogAssertion($"{nameof(type)}が見つかりませんでした。");
    //    Debug.Break();
    //}

    [Button]
    private async UniTask FadeIn()
    {
        _fadeImage.enabled = true;

        while (alfa > 0)
        {
            Debug.Log(alfa);
            alfa -= Speed * Time.deltaTime;
            Alpha();
            await UniTask.Yield();

        }
        alfa = 0;
        Alpha();
        _fadeImage.enabled = false;
    }

    [Button]
    public async UniTask FadeOut()
    {
        _fadeImage.enabled = true;
        while (alfa < 1)
        {
            alfa += Speed * Time.deltaTime;
            Alpha();
            await UniTask.Yield();
        }
        alfa = 1;
        Alpha();
        //_fadeImage.enabled = false;
    }

    private void Alpha()
    {
        _fadeImage.color = new Color(red, green, blue, alfa);
    }
}

