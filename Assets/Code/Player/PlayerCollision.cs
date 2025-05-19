using UnityEngine;

public class PlayerCollision : MonoBehaviour {
    //Palietus kliūtį, žaidimas pasibaigia
    [SerializeField] GameObject playerLJ;
    [SerializeField] GameObject playerDJ;
    [SerializeField] GameObject playerTP;

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.transform.CompareTag("Obstacle")) {
            GameManager.Instance.GameOver();
        }
    }

    //Palietus pinigą, yra paleidžiamas įvykis ir pinigas sunaikinamas
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.CompareTag("Coin")) {
            GameManager.Instance.CoinCollected();
            Destroy(other.gameObject);
        }
    }

    //Jei žaidėjas iškrenta iš žaidimo, jo pozicija yra atstatoma
    private void Update() {
        if (playerLJ.transform.position.x != -5f || playerLJ.transform.position.y > 10f || playerLJ.transform.position.y < -6f) {
            playerLJ.transform.position = new Vector3(-5f, 5f);
        }
        if (playerDJ.transform.position.x != -5f || playerDJ.transform.position.y > 10f || playerDJ.transform.position.y < -6f) {
            playerDJ.transform.position = new Vector3(-5f, 5f);
        }
        if (playerTP.transform.position.x != -5f || playerTP.transform.position.y > 10f || playerTP.transform.position.y < -6f) {
            playerTP.transform.position = new Vector3(-5f, 5f);
        }
    }
}