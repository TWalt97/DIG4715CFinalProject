using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextTrigger : MonoBehaviour
{
    public ObjectiveTextController text;
    public string objectiveText;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            if (text.objectiveText != objectiveText)
            {
                text.objectiveText = (objectiveText);
                text.StartCoroutine(text.StartFade(text.text, 1f, text.visibleColor));
            }
        }
    }
}