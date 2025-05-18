using UnityEngine;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour {
    //meniu ir žaidimo muzikos, mygtuko paspaudimo, mirties, pinigo ir pašokimo garso efektai
    [SerializeField] AudioSource menuMusic;
    [SerializeField] AudioSource gameMusic;
    [SerializeField] AudioSource deathSound;
    [SerializeField] AudioSource coinSound;
    [SerializeField] AudioSource jumpSound;

    //Garso nustatymų komponentas
    [SerializeField] AudioMixer audioMixer;

    //Paleidžiama meniu muzika
    public void StartMenuMusic() {
        gameMusic.Stop();
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
    }

    public void SetEffectsVolume(float volume) {
        audioMixer.SetFloat("EffectsVolume", volume);
    }
}