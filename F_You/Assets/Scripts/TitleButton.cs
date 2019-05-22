using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButton : MonoBehaviour
{
    public AudioClip SE;
    AudioSource buttonSE;

    void Start()
    {
        buttonSE = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    public void Button()
    {
        Invoke("StartGame", 1);
        //ボタンを点滅させるのならInvokeで、いらないならStartGame関数をそのまま使ってもいい

    }
    public void PlaySE() {
        buttonSE.PlayOneShot(SE);
    }


    private void StartGame()
    {
        SceneManager.LoadScene("GameScenes");
    }

    //public void Quit()
    //{
    //    Application.Quit();
    //}

    void Update()
    {
    }
}
