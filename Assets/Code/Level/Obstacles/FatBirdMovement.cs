using UnityEngine;

public class FatBirdMovement : ObstacleMovement {
    private void OnCollisionEnter2D(Collision2D other) {
        //Fizikos komponentui suteikiamos masės ir gravitacijos reikšmės
        //Palietus žemę, kliūtis juda tiesiai
        if (other.transform.CompareTag("Ground")) {
            rb.mass = 100f;
            rb.gravityScale = -0.001f;
            rb.linearVelocityX = Random.Range(-6.5f, -2f);
        }
    }
}