using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFadeIn : MonoBehaviour
{
    [SerializeField]
    private Image fadeInImage;

    public void FadeUI(float fadeDuration)
    {
        StartCoroutine(FadeIn(fadeDuration));
    }

    IEnumerator FadeIn(float fadeTime)
    {
        float curTime = 0;
        while (curTime < fadeTime)
        {
            float lerp = Mathf.Lerp(1, 0, curTime/fadeTime);
            Color imgColor = fadeInImage.color;
            imgColor.a = lerp;
            fadeInImage.color = imgColor;
            curTime = Time.deltaTime;
            yield return null;
        }

        yield return null;
    }
}