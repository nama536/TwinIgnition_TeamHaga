using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrangition : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }

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
