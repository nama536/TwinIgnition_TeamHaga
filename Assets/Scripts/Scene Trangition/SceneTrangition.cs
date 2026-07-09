using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class SceneTrangition : MonoBehaviour
{
    //float Speed = 0.02f;        //フェードするスピード
    //float red, green, blue, alfa;

    //public bool Out = false;
    //public bool In = false;

    //[SerializeField] Image fadeImage;                //パネル


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //fadeImage = GetComponent<Image>();
        //red = fadeImage.color.r;
        //green = fadeImage.color.g;
        //blue = fadeImage.color.b;
        //alfa = fadeImage.color.a;

    }

    // Update is called once per frame
    void Update()
    {
        //if (In)
        //{
        //    FadeIn();
        //}

        //if (Out)
        //{
        //    FadeOut();
        //}

    }
    public void GameScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }

    //void FadeIn()
    //{
    //    alfa -= Speed;
    //    Alpha();
    //    if (alfa <= 0)
    //    {
    //        In = false;
    //        fadeImage.enabled = false;
    //    }
    //}

    //void FadeOut()
    //{
    //    fadeImage.enabled = true;
    //    alfa += Speed;
    //    Alpha();
    //    if (alfa >= 1)
    //    {
    //        Out = false;
    //    }
    //}

    //void Alpha()
    //{
    //    fadeImage.color = new Color(red, green, blue, alfa);
    //}



    // ボタンが押されたときに呼び出す関数
    public void QuitGame()
    {
    #if UNITY_EDITOR
            // Unityエディタ上で実行中の場合は、再生モードを終了する
            UnityEditor.EditorApplication.isPlaying = false;
    #else
                // 実際にビルドされたゲームの場合は、アプリを終了する
                Application.Quit();
    #endif
    }
}
