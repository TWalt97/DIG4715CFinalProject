using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsController : MonoBehaviour
{
    public Slider _musicSlider, _sfxSlider;

    public TextMeshProUGUI musicPercent, sfxPercent;

    // private void Start()
    // {
    //     _musicSlider.value = SaveStuff.musicVolume;
    //     _sfxSlider.value = SaveStuff.sfxVolume;
    // }

    private void Start()
    {
        _musicSlider.value = SaveValues.musicVolume;
        _sfxSlider.value = SaveValues.sfxVolume;


    }

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }
    
    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();
    }

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
}
