using UnityEngine;

public class CoinPosition : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        float posY = Random.Range(1f, 8f);
        rb.transform.position = new Vector2(20f, posY);
    }
}