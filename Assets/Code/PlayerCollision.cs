using UnityEngine;

public class PlayerCollision : MonoBehaviour {
    private void Start() {
        //Pradedant žaidimą, aktyvuojamas žaidėjas
        GameManager.Instance.onPlay.AddListener(ActivatePlayer);
    }

    private void ActivatePlayer() {
        //Aktyvuojamas žaidėjo objektas
        gameObject.SetActive(true);
    }
    private void OnCollisionEnter2D(Collision2D other) {
        //Palietus kliūtį, žaidimas pasibaigia
        if (other.transform.CompareTag("Obstacle")) {
            GameManager.Instance.GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //Palietus coin 
        if (other.transform.CompareTag("Coin")) {
            GameManager.Instance.CoinCollected();
            Destroy(other.gameObject);
        }

        //palietus powerup
        if (other.transform.CompareTag("PowerUp")) {
            GameManager.Instance.PowerCollected();
            Destroy(other.gameObject);
        }
    }
}