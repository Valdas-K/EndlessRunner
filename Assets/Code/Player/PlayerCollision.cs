using UnityEngine;

public class PlayerCollision : MonoBehaviour {
    [SerializeField] SwitchPlayer player;

    private void OnCollisionEnter2D(Collision2D other) {
        //Palietus kliūtį, žaidimas pasibaigia
        if (other.transform.CompareTag("Obstacle")) {
            GameManager.Instance.GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //Palietus pinigą, yra paleidžiamas įvykis ir objektas sunaikinamas
        if (other.transform.CompareTag("Coin")) {
            GameManager.Instance.CoinCollected();
            Destroy(other.gameObject);
        }
    }

    private void Update() {
        //Jei žaidėjas iškrenta iš žaidimo, jo pozicija yra atstatoma
        if (player.transform.position.x != -5f || player.transform.position.y > 10f || player.transform.position.y < -6f) {
            player.transform.position = new Vector3(-5f, 4.5f, 0f);
        }
    }
}