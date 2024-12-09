using System.Collections;
using UnityEngine;

public class TimePowerUp : PowerUp {
    protected override void StartLogic(Collider2D player) {
        //Paleidžiame korutina
        StartCoroutine(Pickup());
    }

    public IEnumerator Pickup() {
        //Paleidžiamas garsas, vykdomas efektas, išjungiami komponentai
        sound.Play();
        Time.timeScale *= multiplier;
        ChangeFactors();

        //Pasibaigus laikui, efektai atgauna pradines reikšmes ir pastiprinimas sunaikinamas
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale /= multiplier;
        Destroy(gameObject);
    }
}