using UnityEngine;
public class BatMovement : MonoBehaviour
{
    //Aprašomas kliūties fizikos komponentas
    private Rigidbody2D rb;

    private void Awake() {
        //Pridedamas kliūties fizikos komponentas
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        float mass = Random.Range(20f, 90f) / 10000;
        float grav = Random.Range(30f, 90f) / 1000;
        float speed = Random.Range(-9f, -3f);

        rb.mass = mass;
        rb.gravityScale = grav;
        rb.linearVelocityX = speed;
    }
}