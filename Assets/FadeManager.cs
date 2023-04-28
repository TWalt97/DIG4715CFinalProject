using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    [SerializeField]
    private Image fadeImage;

    [SerializeField]
    private float fadeTime = 2f;
    [SerializeField]
    private float timeUntilFadeBack = 5f;

    private void Start()
    {
        AudioManager.Instance.musicSource.Stop();
        AudioManager.Instance.PlayMusic("WinMusic");
        StartCoroutine(Fade(timeUntilFadeBack, fadeTime));
    }

    IEnumerator Fade(float timeUntilFadeBack, float fadeTime)
    {
        float currentTime = 0;
        while (currentTime < fadeTime)
        {
            currentTime += Time.deltaTime;
            float fadeAlpha = Mathf.Lerp(1, 0, currentTime / fadeTime);
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fadeAlpha);
            yield return null;
        }

        yield return new WaitForSeconds(timeUntilFadeBack);

        float newCurrentTime = 0;
        while (newCurrentTime < fadeTime)
        {
            newCurrentTime += Time.deltaTime;
            float fadeAlpha = Mathf.Lerp(0, 1, newCurrentTime / fadeTime);
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fadeAlpha);
            yield return null;
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        yield return null;
    }
}
