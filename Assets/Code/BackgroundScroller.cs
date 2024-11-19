using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    //Aprašomi foninės nuotraukos komponentai
    public BoxCollider2D collider2d;
    public Rigidbody2D rb;
    
    //Nuotraukos pločio kintamasis
    private float width;

    //Fono judėjimo greičio kintamasis
    public float scrollSpeed;

    private void Start()
    {
        //Pridedami foninės nuotraukos komponentai
        collider2d = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        //Randamas foninės nuotraukos plotis
        width = collider2d.size.x;

        //Toliau collider komponento nebereikia, todėl jis yra išjungiamas. Sumažės atliekamų skaičiavimų ir naudojamos atminties kiekiai
        collider2d.enabled = false;

        //Fonui suteikiamas judėjimo greitis: x reikšmei suteikiamas "scrollSpeed" kintamasis, y - 0, nes y ašyje fonas nejuda
        rb.linearVelocity = new Vector2(scrollSpeed, 0);
    }

    private void Update()
    {
        //Suteikiamas besikartojančio fono efektas
        if (transform.position.x < -width) {
            //Pasibaigus pirmai fono nuotraukai, yra atkartojamas paveikslėlis
            Vector2 resetPosition = new(width * 2f, 0);
            transform.position = (Vector2)transform.position + resetPosition;
        }
    }
}