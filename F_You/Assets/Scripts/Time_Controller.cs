using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
////////////////////////////////////////////////////////
//ゲームの結果のシーンや何かを作る前までの仮のデータ参照
using UnityEngine.SceneManagement;
////////////////////////////////////////////////////////

public class Time_Controller : MonoBehaviour{

    public float LimitTime;
    public Text text_Timer;

    void Start()
    {
        LimitTime = 60;
    }
    // Update is called once per frame
    void Update()
    {
        LimitTime -= Time.deltaTime;
        text_Timer.text = "Time : " + Mathf.Round(LimitTime);
        ////////////////////////////////////////////////////////
            //ゲームの結果のシーンや何かを作る前までの仮のデータ参照
        if (LimitTime <= 0) {
            SceneManager.LoadScene("Title");
        }
        ////////////////////////////////////////////////////////
    }
}
