using System.Collections;
using UnityEngine;

//brackeys
public class PowerUpLogic : MonoBehaviour {
    //Aprašomi kintamieji:
    //pastiprinimo pavadinimas,
    //sustiprinimo modifikacijos reikšmės (multipliers),
    //sustiprinimo veikimo laikas
    private string powerUpName;
    public float sizeMultiplier;
    public float sizeDuration;
    public float timeMultiplier;
    public float timeDuration;

    //Palietus žaidėją, paleidžiamas metodas
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            powerUpName = name.Replace("(Clone)", "");
            StartCoroutine(Pickup(other));
        }
    }

    //Tikrinama, kuris pastiprinamas paimtas ir vykdomas efektas
    IEnumerator Pickup(Collider2D player) {
        GameManager.Instance.PowerCollected();
        if (powerUpName == "SizePowerUp") {
            player.transform.localScale *= sizeMultiplier;
            ChangeFactors();
            yield return new WaitForSecondsRealtime(sizeDuration);
            player.transform.localScale /= sizeMultiplier;
        }
        if (powerUpName == "TimePowerUp") {
            Time.timeScale *= timeMultiplier;
            ChangeFactors();
            yield return new WaitForSecondsRealtime(timeDuration);
            Time.timeScale /= timeMultiplier;
        }
        Destroy(gameObject);
    }

    //Pakeičiamos reikšmės
    private void ChangeFactors() {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }
}