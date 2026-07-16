using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    [System.Serializable]
    public class SoundData
    {
        public string name;
        public AudioClip clip;
    }

    [System.Serializable]
    public class BGMData
    {
        public string name;
        public AudioClip clip;
    }

    [SerializeField] private SoundData[] soundDatas;
    private AudioSource[] audioSourceList = new AudioSource[10];
    private Dictionary<string, SoundData> soundDictionary = new Dictionary<string, SoundData>();

    [SerializeField] private BGMData[] bgmDatas;
    private AudioSource bgmAudioSource;
    private Dictionary<string, BGMData> bgmDictionary = new Dictionary<string, BGMData>();

    public static SoundManager Instance;
    private void Awake()
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

        bgmAudioSource = gameObject.AddComponent<AudioSource>();
        bgmAudioSource.loop = true;

        for (var i = 0; i < audioSourceList.Length; i++)
        {
            audioSourceList[i] = gameObject.AddComponent<AudioSource>();
        }

        foreach (var soundData in soundDatas)
        {
            soundDictionary.Add(soundData.name, soundData);
        }

        foreach (var bgmData in bgmDatas)
        {
            bgmDictionary.Add(bgmData.name, bgmData);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "TitleScene":
                PlayBGM("TitleScene", 0.5f);
                break;

            case "TestSoundScene":
                PlayBGM("TestSoundScene", 1f);
                break;

            case "ResultScene":
                PlayBGM("ResultScene", 0.3f);
                break;

            case "MainScene":
                PlayBGM("MainScene", 0.3f);
                break;

            default:
                StopBGM();
                break;
        }

    }

    private AudioSource GetUnAudioSours()
    {
        for (var i = 0; i < audioSourceList.Length; i++)
        {
            if (audioSourceList[i].isPlaying == false)
            {
                return audioSourceList[i];
            }


        }
        return null;
    }


    public void Play(AudioClip clip)
    {
        var audioSource = GetUnAudioSours();
        if (audioSource == null) return;
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void Play(string name)
    {
        if (soundDictionary.TryGetValue(name, out var soundData))
        {
            Play(soundData.clip);
        }
        else
        {
            Debug.LogWarning($"Sound with name '{name}' not found.");
        }
    }

    public void PlayBGM(string name, float volume)
    {
        if (!bgmDictionary.TryGetValue(name, out var bgmData))
        {
            return;
        }

        if (bgmAudioSource.clip == bgmData.clip && bgmAudioSource.isPlaying)
        {
            return;
        }

        bgmAudioSource.clip = bgmData.clip;
        bgmAudioSource.volume = volume;
        bgmAudioSource.Play();


    }

    public void StopBGM()
    {
        bgmAudioSource.Stop();
        bgmAudioSource.clip = null;
    }



}
