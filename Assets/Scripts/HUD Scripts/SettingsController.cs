using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SettingsController : MonoBehaviour
{
    public Slider _musicSlider, _sfxSlider, _spookySlider;
    public TextMeshProUGUI musicPercent, sfxPercent, spookyPercent;

    public GameObject fullscreenToggle; 
    public GameObject godModeToggle; 

    private GameObject godModeCanvas;
    // public Dropdown dropDown;

    // private void Start()
    // {
    //     _musicSlider.value = SaveStuff.musicVolume;
    //     _sfxSlider.value = SaveStuff.sfxVolume;
    // }

    // void OnEnable()
    // {
    //     SceneManager.sceneLoaded += OnLevelFinishedLoading;
    // }

    // void OnDisable()
    // {
    //     SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    // }

    // void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    // {
    //     Debug.Log("a");
    //     godModeCanvas = DontDestroy.Instance.gameObject;
    // }

    private void Awake()
    {
        godModeCanvas = DontDestroy.Instance.gameObject;
        _musicSlider.value = SaveValues.musicVolume;
        _sfxSlider.value = SaveValues.sfxVolume;
        _spookySlider.value = SaveValues.spookyPercent;

        fullscreenToggle.GetComponent<Toggle>().isOn = SaveValues.isFullscreen;
        godModeToggle.GetComponent<Toggle>().isOn = SaveValues.isGodMode;

        // godModeCanvas = GameObject.FindGameObjectWithTag("GodMode");

        // dropDown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropDown); });
    }

    // In case we want to add mute buttons
    // public void ToggleMusic()
    // {
    //     AudioManager.Instance.ToggleMusic();
    // }
    
    // public void ToggleSFX()
    // {
    //     AudioManager.Instance.ToggleSFX();
    // }

    public void MusicVolume()
    {
        musicPercent.text = Mathf.RoundToInt(_musicSlider.value * 100) + "%";
        AudioManager.Instance.MusicVolume(_musicSlider.value);
        SaveValues.musicVolume = _musicSlider.value;
    }

    public void SFXVolume()
    {
        sfxPercent.text = Mathf.RoundToInt(_sfxSlider.value * 100) + "%";
        AudioManager.Instance.SFXVolume(_sfxSlider.value); 
        SaveValues.sfxVolume = _sfxSlider.value;

    }

    public void SpookySlider()
    {
        spookyPercent.text = Mathf.RoundToInt(_spookySlider.value * 100) + "%";
        // Insert reference to change float value in player control that controls light intensity here
        SaveValues.spookyPercent = _spookySlider.value;
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }


    public void SetFullScreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        SaveValues.isFullscreen = isFullscreen;   
    }

    public void SetGodMode (bool isGod)
    {
        SaveValues.isGodMode = isGod;   
        // GameObject godModeCanvas = GameObject.FindGameObjectWithTag("GodMode");

        if (isGod)
        {
            // godModeCanvas = GameObject.Find("GameJournalistCanvas");
            godModeCanvas.SetActive(true);
        }
        else
        {
            // godModeCanvas = GameObject.Find("GameJournalistCanvas");
            godModeCanvas.SetActive(false);
        }

    }

    // public void DropdownItemSelected(Dropdown dropDown)
    // {
    //     switch(dropDown.value)
    //     {
    //         case 0: PlayerPrefs.SetInt("DROPDOWNQUALITY", 0);
    //             break;
    //         case 1: PlayerPrefs.SetInt("DROPDOWNQUALITY", 1);
    //             break;
    //         case 2: PlayerPrefs.SetInt("DROPDOWNQUALITY", 2);
    //             break;
    //     }
    // }
}
