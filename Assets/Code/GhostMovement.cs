using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    //Aprašomas kliūties fizikos komponentas
    private Rigidbody2D rb;

    private void Awake()
    {
        //Pridedamas kliūties fizikos komponentas
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        float mass = Random.Range(300f, 4000f);
        float grav = Random.Range(-100f, -30f) / 1000;
        float speed = Random.Range(-12f, -3f);

        rb.mass = mass;
        rb.gravityScale = grav;
        rb.linearVelocityX = speed;
    }
}
