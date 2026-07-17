using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _resultText, _scoreText;
    [SerializeField] Button _titleButton, _retryButton;

    void Start()
    {
        StartCoroutine(StartResult());
    }

    private IEnumerator StartResult()
    {
        yield return new WaitForSeconds(1f);
        if (MaingameManager.Instance.DoClear) _resultText.text = "Clear!";
        else _resultText.text = "Failure";
        _resultText.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        _scoreText.text = "Score: " + MaingameManager.Instance.Score.ToString();
        _scoreText.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        _titleButton.gameObject.SetActive(true);
        _retryButton.gameObject.SetActive(true);
        _titleButton.Select();

        MaingameManager.Instance.DoClear = false;
        MaingameManager.Instance.Score = 0;
    }

    public void TitleButton()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void RetryButton()
    {
        SceneManager.LoadScene("MainScene");
    }
}
