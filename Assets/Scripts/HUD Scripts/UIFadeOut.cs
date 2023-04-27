using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIFadeOut : MonoBehaviour
{
    // public static UIFadeOut Instance;

    [SerializeField]
    private Image fadeOutImage;

    public void FadeUI(float fadeDuration)
    {
        StartCoroutine(FadeOut(fadeDuration));
    }

    IEnumerator FadeOut(float fadeTime)
    {
        float curTime = 0;
        while (curTime < fadeTime)
        {
            float lerp = Mathf.Lerp(0, 1, curTime/fadeTime);
            Color imgColor = fadeOutImage.color;
            imgColor.a = lerp;
            fadeOutImage.color = imgColor;
            curTime += Time.deltaTime;
            yield return null;
        }

        Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        yield return null;
    }
}
