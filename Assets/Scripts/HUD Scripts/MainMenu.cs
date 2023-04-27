using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        AudioManager.Instance.musicSource.Stop();
        AudioManager.Instance.PlayMusic("HubMusic");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

