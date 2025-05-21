using System;
using UnityEngine;
using UnityEngine.Rendering;

public class SettingsData : MonoBehaviour
{
    [SerializeField] SettingsController settings;
    public void SaveSettingsData()
    {

    }

    public void LoadSettingsData() {
        //Užkraunami nustatymai kiekvienam mygtukui
        //Jei nustatymai nėra išsaugoti, yra priskiriama reikšmė
        if (PlayerPrefs.HasKey("JumpKey")) {
            settings.input.jumpKey = (KeyCode)PlayerPrefs.GetInt("JumpKey");
        } else {
            settings.input.jumpKey = KeyCode.Space;
        }

        if (PlayerPrefs.HasKey("PauseKey")) {
            settings.input.pauseKey = (KeyCode)PlayerPrefs.GetInt("PauseKey");
        } else {
            settings.input.pauseKey = KeyCode.Escape;
        }

        //Ekrano režimo nustatymai
        if (PlayerPrefs.HasKey("fullScreen")) {
            Screen.fullScreen = Convert.ToBoolean(PlayerPrefs.GetInt("fullScreen"));
        } else {
            Screen.fullScreen = true;
        }
        if (PlayerPrefs.HasKey("ResolutionX") && PlayerPrefs.HasKey("ResolutionY")) {
            if (Screen.fullScreen == true) {
                Screen.SetResolution(PlayerPrefs.GetInt("ResolutionX"), PlayerPrefs.GetInt("ResolutionY"), FullScreenMode.FullScreenWindow, Screen.currentResolution.refreshRateRatio);
            } else {
                Screen.SetResolution(PlayerPrefs.GetInt("ResolutionX"), PlayerPrefs.GetInt("ResolutionY"), FullScreenMode.Windowed, Screen.currentResolution.refreshRateRatio);
            }
        }

        //Muzika ir efektai
        if (PlayerPrefs.HasKey("MusicVolume")) {
            settings.sound.audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
        } else {
            settings.sound.audioMixer.SetFloat("MusicVolume", -20f);
        }

        if (PlayerPrefs.HasKey("EffectsVolume")) {
            settings.sound.audioMixer.SetFloat("EffectsVolume", PlayerPrefs.GetFloat("EffectsVolume"));
        } else {
            settings.sound.audioMixer.SetFloat("EffectsVolume", -20f);
        }



    }
}
