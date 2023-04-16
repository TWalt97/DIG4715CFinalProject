using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PauseUi : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public GameObject mainPauseScreen;
    public GameObject creditsScreen;
    public GameObject howToPlayScreen;
    public GameObject HUDOverlay;

    bool paused = false;
    public bool hud;
    public CinemachineBrain cinemachineBrain;

    //public GameObject ResumeButtonMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            paused = !paused;
            if (paused == true)
            {
                Time.timeScale = 0;
                AudioListener.pause = true;
                pauseMenuUI.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                //cinemachineBrain.m_UpdateMethod = CinemachineBrain.UpdateMethod.LateUpdate;

                mainPauseScreen.SetActive(true);
                creditsScreen.SetActive(false);
                howToPlayScreen.SetActive(false);
                HUDOverlay.SetActive(false);
            }
            else
            {
                Time.timeScale = 1;
                AudioListener.pause = false;
                pauseMenuUI.SetActive(false);
                HUDOverlay.SetActive(true);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                //cinemachineBrain.m_UpdateMethod = CinemachineBrain.UpdateMethod.SmartUpdate;
            }
        }


        //The original version of pause, temporarily commenting it out until we find a fix.
        /*if (Input.GetKeyDown(KeyCode.P) && !GameIsPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }*/
    }

    // resume game
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        HUDOverlay.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // change to about
    public void HudTeleport()
    {
        hud = true;
    }

    // pause game
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Debug.Log("Resume");
    }
}
