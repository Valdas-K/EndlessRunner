using UnityEngine;

public class SettingsController : MonoBehaviour {
    public InputSettings input;
    [SerializeField] SettingsData data;
    public ScreenSettings screen;
    public MusicController sound;

    void Start() {
        //Užkraunami visi išsaugoti nustatymai
        LoadSettings();
    }

    public void LoadSettings() {
        data.LoadSettingsData();
    }

    public void ChangeJumpButton() {
        input.StartKeyChange("Jump");
    }

    public void ChangeFullScreen(bool isFullScreen) {
        screen.SetFullScreen(isFullScreen);
    }

    public void ChangePauseButton() {
        input.StartKeyChange("Pause");
    }

    public void ChangeMusicVolume(float volume) {
        sound.SetMusicVolume(volume);
    }

    public void ChangeEffectsVolume(float volume) {
        sound.SetEffectsVolume(volume);
    }

    public void ChangeResolution(int index) {
        screen.SetResolution(index);
    }
}