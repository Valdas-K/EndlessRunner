using System.Collections;
using UnityEngine;

public class TimePowerUp : PowerUp {
    protected override void StartLogic(Collider2D player) {
        //Paleidžiame korutina
        StartCoroutine(Pickup(player));
    }

    public IEnumerator Pickup(Collider2D player) {
        player.GetComponentInParent<SwitchPlayer>();

        //Paleidžiamas garsas, vykdomas efektas, išjungiami komponentai
        sound.Play();
        Time.timeScale *= multiplier;

        if (player.GetComponent<LongPlayerMovement>() != null) {
            player.GetComponent<LongPlayerMovement>().jumpForce = 169f;
        }
        ChangeFactors();


        //Pasibaigus laikui, efektai atgauna pradines reikšmes ir pastiprinimas sunaikinamas
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale /= multiplier;
        if (player.GetComponent<LongPlayerMovement>() != null) {
            player.GetComponent<LongPlayerMovement>().jumpForce = 13f;
        }
        Destroy(gameObject);
    }
}