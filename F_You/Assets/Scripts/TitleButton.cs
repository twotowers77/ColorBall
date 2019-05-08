using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void Button()
    {
        Invoke("StartGame", 0f);
        //ボタンを点滅させるのならInvokeで、いらないならStartGame関数をそのまま使ってもいい

    }

    private void StartGame()
    {
        SceneManager.LoadScene("GameScenes");
    }

    public void Quit()
    {
        Application.Quit();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) SceneManager.LoadScene("Title");
    }
}
