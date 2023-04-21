using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlVolume : MonoBehaviour
{
    void Update()
    {
        gameObject.GetComponent<AudioSource>().volume = SaveValues.sfxVolume;
    }
}
