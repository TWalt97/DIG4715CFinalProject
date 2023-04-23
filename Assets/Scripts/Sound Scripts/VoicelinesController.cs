using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VoicelinesController : MonoBehaviour
{

    public static VoicelinesController Instance;
    
    public Sound[] voicelineSounds;

    public AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // PlayVoiceline("Welcome");
    }

    public void PlayVoiceline(string name)
    {
        Sound s = Array.Find(voicelineSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            audioSource.PlayOneShot(s.clip);
        }
    }
}
