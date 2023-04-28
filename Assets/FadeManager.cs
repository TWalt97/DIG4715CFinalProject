using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    [SerializeField]
    private UIFadeIn fadeIn;

    void Awake()
    {
        fadeIn.FadeUI(5f);
    }
}
