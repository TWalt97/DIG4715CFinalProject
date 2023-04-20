using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveTextController : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Color visibleColor = new Color(1, 1, 1, 1);
    Color invisibleColor = new Color(1, 1, 1, 0);
    public string objectiveText;
    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
        //StartCoroutine(StartFade(text, 1f, visibleColor));
    }

    void Update()
    {

    }


    public IEnumerator StartFade(TextMeshProUGUI text, float duration, Color targetColor)
    {
        float currentTime = 0;
        Color start = text.color;
        text.SetText(objectiveText);
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            text.color = Color.Lerp(start, targetColor, currentTime / duration);
            yield return null;
        }
        yield return new WaitForSeconds(3);
        currentTime = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            text.color = Color.Lerp(targetColor, start, currentTime / duration);
            yield return null;
        }
        yield break;
    }


}