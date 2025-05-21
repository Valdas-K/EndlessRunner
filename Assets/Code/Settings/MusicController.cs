using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class MusicController : MonoBehaviour {
    //meniu ir žaidimo muzikos, mygtuko paspaudimo, mirties, pinigo ir pašokimo garso efektai
    [SerializeField] AudioSource menuMusic;
    AudioSource gameMusic;
    [SerializeField] AudioSource sandyDessertMusic;
    [SerializeField] AudioSource spookyForrestMusic;
    [SerializeField] AudioSource pixelCityMusic;
    [SerializeField] AudioSource deathSound;
    [SerializeField] AudioSource coinSound;
    [SerializeField] AudioSource jumpSound;

    //Garso nustatymų komponentas
    public AudioMixer audioMixer;

    public Slider musicSlider;
    public Slider effectsSlider;

    private void Start()
    {
        UpdateSliders();
    }

    public void ChangeGameMusic(int id) {
        if (id == 0) gameMusic = sandyDessertMusic; else if (id == 1) gameMusic = spookyForrestMusic; else gameMusic = pixelCityMusic;
    }

    //Paleidžiama meniu muzika
    public void StartMenuMusic() {
        menuMusic.time = Random.Range(0f, menuMusic.clip.length);
        menuMusic.Play();
    }

    //Paleidžiama lygio muzika
    public void StartGameMusic() {
        menuMusic.Stop();
        gameMusic.time = Random.Range(0f, gameMusic.clip.length);
        gameMusic.Play();
    }

    //Sustabdoma visa muzika
    public void StopAllMusic() {
        menuMusic.Stop();
        gameMusic.Stop();
    }

    //Paleidžiamas mirties efektas
    public void PlayDeathSound() {
        deathSound.Play();
    }

    //Paleidžiamas pinigo efektas
    public void PlayCoinSound() {
        coinSound.Play();
    }

    //Paleidžiamas pašokimo efektas
    public void PlayJumpSound() {
        jumpSound.Play();
    }

    //Atnaujinamas muzikos ir efektų nustatymai
    public void SetMusicVolume(float volume) {
        audioMixer.SetFloat("MusicVolume", volume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetEffectsVolume(float volume) {
        audioMixer.SetFloat("EffectsVolume", volume);
        PlayerPrefs.SetFloat("EffectsVolume", volume);
    }

    public void UpdateSliders() {
        float musicValue;
        bool result = audioMixer.GetFloat("MusicVolume", out musicValue);
        if (result) {
            musicSlider.value = musicValue;
        } else {
            musicSlider.value = -20f;
        }

        float effectsValue;
        result = audioMixer.GetFloat("EffectsVolume", out effectsValue);
        if (result) {
            effectsSlider.value = effectsValue;
        } else {
            effectsSlider.value = -20f;
        }
    }
}