using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinOrLoseMenu : MonoBehaviour
{
    public void Awake()
    {
        // Cursor.lockState = CursorLockMode.None;
    }
    public void MainMenu()
    {
        AudioManager.Instance.musicSource.Stop();
        AudioManager.Instance.PlayMusic("MenuMusic");
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
