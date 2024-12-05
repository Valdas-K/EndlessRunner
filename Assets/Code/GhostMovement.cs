using UnityEngine;

public class GhostMovement : ObstacleMovement {
    private void Start() {
        //Fizikos komponentui suteikiamos reikšmės
        rb.mass = Random.Range(300f, 4000f);
        rb.gravityScale = Random.Range(-100f, -30f) / 1000;
        rb.linearVelocityX = Random.Range(-11f, -3f);
    }
}