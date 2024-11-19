using UnityEngine;

public class FatBirdMovement : MonoBehaviour
{
    //Aprašomas kliūties fizikos komponentas
    private Rigidbody2D rb;

    private void Awake() {
        //Pridedamas kliūties fizikos komponentas
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        //Palietus žemę, kliūtis juda tiesiai
        if (other.transform.CompareTag("Ground")) {
            rb.gravityScale = -0.001f;
            rb.mass = 100f;
            rb.linearVelocityX = -7f;
        }
    }
}