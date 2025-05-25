using UnityEngine;

public class BatMovement : ObstacleMovement {
    private void Start() {
        //Fizikos komponentui suteikiamos masės ir gravitacijos reikšmės
        rb.mass = Random.Range(20f, 90f) / 10000;
        rb.gravityScale = Random.Range(35f, 95f) / 1000;
    }
}