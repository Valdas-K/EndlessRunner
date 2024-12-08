using UnityEngine;

public class ObstacleSound : MonoBehaviour {
    //Aprašomi garso komponento, laiko tarp garsų kintamieji
    [SerializeField] AudioSource obstacleSound;
    [SerializeField] float soundTime;
    private float soundTimer = 0f;

    private void Update() {
        //Kaupiamas laikas iki kito garso paleidimo
        soundTimer += Time.deltaTime;
        if (soundTimer >= soundTime) {
            //Paleidžiamas kliūties garsas ir laikmatis atstatomas į 0
            PlaySound();
            soundTimer = 0f;
        }
    }
    public void PlaySound() {
        //Paleidžiamas kliūties garsas
        obstacleSound.Play();
    }
}