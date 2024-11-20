using System.Collections;
using UnityEngine;

//brackeys
public class PowerUpLogic : MonoBehaviour {
    private string powerUpName;
    public float sizeMultiplier = 0.5f;
    public float sizeDuration = 3f;

    public float timeMultiplier = 2f;
    public float timeDuration = 2f;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            powerUpName = name.Replace("(Clone)", "");
            StartCoroutine(Pickup(other));
        }
    }

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

    private void ChangeFactors() {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }
}