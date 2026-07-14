using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SoundTest : MonoBehaviour
{
    [SerializeField] private SoundManager soundManager;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))//左クリックで再生
        {
            soundManager.Play("左クリック");
        }

        if (Input.GetMouseButtonDown(1))//右クリックで再生
        {
            soundManager.Play("右クリック");
            Debug.Log("右クリックで再生siteruyinn");
        }
    }
}
