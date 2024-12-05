using UnityEngine;

public abstract class ObstacleMovement : MonoBehaviour
{
    //Kliūties fizikos komponentas
    protected Rigidbody2D rb;

    private void Awake() {
        //Pridedamas komponentas
        rb = GetComponent<Rigidbody2D>();
    }
}