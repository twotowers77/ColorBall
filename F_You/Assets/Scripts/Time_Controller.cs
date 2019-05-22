using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Time_Controller : MonoBehaviour{

    public GameObject ResultGameUI;
    public bool stopGame = false;
    public float LimitTime;
    public Text text_Timer;

    void Start()
    {
        LimitTime = 60;
        ResultGameUI.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        LimitTime -= Time.deltaTime;
        text_Timer.text = "Time : " + Mathf.Round(LimitTime);
        ////////////////////////////////////////////////////////

        if (LimitTime <= 0) {
            stopGame = true;
            LimitTime = 0;
        }
        if (stopGame)
        {
            ResultGameUI.SetActive(true);
            Time.timeScale = 0;
        }
        if (!stopGame)
        {
            ResultGameUI.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("GameScenes");
    }
    public void TitleMenu()
    {
        SceneManager.LoadScene("Title");
    }
}
