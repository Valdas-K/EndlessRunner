using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void Start()
    {
        //Pradedant žaidimą, aktyvuojamas žaidėjas
        GameManager.Instance.onPlay.AddListener(ActivatePlayer);
    }

    private void ActivatePlayer()
    {
        //Aktyvuojamas žaidėjo objektas
        gameObject.SetActive(true);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        //Palietus kliūtį, žaidimas pasibaigia
        if (other.transform.CompareTag("Obstacle"))
        {
            gameObject.SetActive(false);
            GameManager.Instance.GameOver();
        }
    }
}