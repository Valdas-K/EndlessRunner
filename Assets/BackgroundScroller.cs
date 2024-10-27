using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public BoxCollider2D collider2d;
    public Rigidbody2D rb;
    
    private float width;
    private float scrollSpeed = -2f;

    private void Start()
    {
        collider2d = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        width = collider2d.size.x;

        collider2d.enabled = false;
        rb.linearVelocity = new Vector2(scrollSpeed, 0);
    }
    private void Update()
    {
        if(transform.position.x < -width)
        {
            Vector2 resetPosition = new(width * 2f, 0);
            transform.position = (Vector2)transform.position + resetPosition;
        }
    }
}
