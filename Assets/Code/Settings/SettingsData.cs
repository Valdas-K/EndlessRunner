using System;
using UnityEngine;

public class SettingsData : MonoBehaviour {
    [SerializeField] SettingsController settings;
    private float soundValue;
    public void SaveSettingsData() {
        //Išsaugomi nustatymai, kurie bus išsaugomi ir perkrovus žaidimą
        //Žaidimo valdymas
        PlayerPrefs.SetInt("JumpKey", (int)settings.input.jumpKey);
        PlayerPrefs.SetInt("PauseKey", (int)settings.input.pauseKey);

        //Ekrano režimas ir rezoliucija
        PlayerPrefs.SetInt("fullScreen", Convert.ToInt16(settings.screen.screenTypeToggle.isOn));
        PlayerPrefs.SetInt("ResolutionX", settings.screen.selectedWidth);
        PlayerPrefs.SetInt("ResolutionY", settings.screen.selectedHeight);

        //Muzika ir efektai
        settings.sound.audioMixer.GetFloat("MusicVolume", out soundValue);
        PlayerPrefs.SetFloat("MusicVolume", soundValue);
        settings.sound.audioMixer.GetFloat("EffectsVolume", out soundValue);
        PlayerPrefs.SetFloat("EffectsVolume", soundValue);

        PlayerPrefs.Save();
    }

    public void LoadSettingsData() {
        //Užkraunami visi išsaugoti nustatymai
        //Žaidimo valdymas
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

        //Ekrano režimas ir rezoliucija
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
        } else {
            if (Screen.fullScreen == true) {
                Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, FullScreenMode.FullScreenWindow, Screen.currentResolution.refreshRateRatio);
            } else {
                Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, FullScreenMode.Windowed, Screen.currentResolution.refreshRateRatio);
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

        UpdateSettingsUI();
    }

    public void UpdateSettingsUI() {
        settings.input.UpdateButtonText();
        settings.sound.UpdateSliders();
        settings.screen.UpdateScreenCheck();
        settings.screen.FillResolutionDropdown();
    }
}
