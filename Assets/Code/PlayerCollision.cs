using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    [SerializeField] private AudioSource coinCollectedSound;
    [SerializeField] private AudioSource powerCollectedSound;


    //Palietus kliūtį, žaidimas pasibaigia
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.transform.CompareTag("Obstacle")) {
            GameManager.Instance.GameOver();
        }
    }

    //Palietus pinigą, yra paleidžiamas įvykis ir pinigas sunaikinamas
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.CompareTag("Coin")) {
            GameManager.Instance.CoinCollected();
            coinCollectedSound.Play();
            Destroy(other.gameObject);
        }

        if (other.transform.CompareTag("PowerUp"))
        {
            GameManager.Instance.PowerCollected();
            powerCollectedSound.Play();
        }
    }
}