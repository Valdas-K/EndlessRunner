using UnityEngine;

public class BatMovement : ObstacleMovement {
    private void Start() {
        //Fizikos komponentui suteikiamos masės ir gravitacijos reikšmės
        rb.mass = Random.Range(20f, 90f) / 10000;
        rb.gravityScale = Random.Range(30f, 90f) / 1000;
    }
}