using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
using UnityEngine.InputSystem;

public class PauseUi : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public GameObject mainPauseScreen;
    public GameObject creditsScreen;
    public GameObject howToPlayScreen;
    public GameObject optionsScreen;
    public GameObject HUDOverlay;

    bool paused = false;
    public bool hud;
    public CinemachineBrain cinemachineBrain;
    [SerializeField]
    private TextMeshProUGUI areaText;

    [SerializeField]
    private PlayerInput playerInput;
    private InputAction pauseAction;

    //public GameObject ResumeButtonMenuUI;

    // Update is called once per frame

    private void Awake()
    {
        pauseAction = playerInput.actions["Pause"];
    }
    private void OnEnable()
    {
        pauseAction.performed += _ => Paused();
        pauseAction.Enable();
    }

    private void OnDisable()
    {
        pauseAction.performed -= _ => Paused();
        pauseAction.Disable();
    }

    void Paused()
    {
        if (this != null)
        {
            paused = !paused;
            if (paused == true)
            {
                Time.timeScale = 0;
                AudioListener.pause = true;
                pauseMenuUI.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                areaText.enabled = false;
                //cinemachineBrain.m_UpdateMethod = CinemachineBrain.UpdateMethod.LateUpdate;

                mainPauseScreen.SetActive(true);
                creditsScreen.SetActive(false);
                howToPlayScreen.SetActive(false);
                optionsScreen.SetActive(false);
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
                areaText.enabled = true;
                //cinemachineBrain.m_UpdateMethod = CinemachineBrain.UpdateMethod.SmartUpdate;
            }
        }  
    }

    // resume game
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        HUDOverlay.SetActive(true);
        Time.timeScale = 1f;
        AudioListener.pause = false;
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
