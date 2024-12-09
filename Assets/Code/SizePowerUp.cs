using System.Collections;
using UnityEngine;

public class SizePowerUp : PowerUp {
    protected override void StartLogic(Collider2D player) {
        //Paleidžiame korutina
        StartCoroutine(Pickup(player));
    }

    public IEnumerator Pickup(Collider2D player) {
        //Paleidžiamas garsas, vykdomas efektas, išjungiami komponentai
        mc.PlayPowerSound();
        player.transform.localScale /= multiplier;
        ChangeFactors();

        //Pasibaigus laikui, efektai gauna pradines reikšmes ir pastiprinimas sunaikinamas
        yield return new WaitForSecondsRealtime(duration);
        player.transform.localScale *= multiplier;
        Destroy(gameObject);
    }
}