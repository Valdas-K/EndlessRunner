using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] AudioSource menuMusic;
    [SerializeField] AudioSource gameMusic;
    [SerializeField] AudioSource clickSound;
    [SerializeField] AudioSource deathSound;
    [SerializeField] AudioSource coinSound;
    [SerializeField] AudioSource jumpSound;
    [SerializeField] AudioSource powerSound;


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

    public void StopAllMusic() {
        menuMusic.Stop();
        gameMusic.Stop();
    }

    public void PlayDeathSound() {
        deathSound.Play();
    }

    public void PlayCoinSound() {
        coinSound.Play();
    }

    public void PlayPowerSound()
    {
        powerSound.Play();
    }

    public void PlayJumpSound() {
        jumpSound.Play();
    }

    //Paspaustas bet kuris mygtukas
    public void ClickButton() {
        clickSound.Play();
    }
}