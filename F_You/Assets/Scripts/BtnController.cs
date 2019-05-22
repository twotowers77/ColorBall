using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnController : MonoBehaviour
{
    public AudioClip SE;
    AudioSource buttonSE;
    public GameObject PauseUI;
    public bool paused = false;

    // Start is called before the first frame update
    void Start() {
        PauseUI.SetActive(false);
        buttonSE = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
        }
        if (paused)
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0;
        }
        if (!paused)
        {
            PauseUI.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    public void PlaySE()
    {
        buttonSE.PlayOneShot(SE);
    }
    public void Resume()
    {
        paused = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene("GameScenes");
    }
    public void TitleMenu()
    {
        SceneManager.LoadScene("Title");
    }
    public void Quit()
    {
        Application.Quit();
    }
}