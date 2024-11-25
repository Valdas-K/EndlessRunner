using UnityEngine;

public class PickupPosition : MonoBehaviour {
    //Aprašomas objekto fizikos komponentas
    private Rigidbody2D rb;

    //X ir Y reikšmės
    float posY, posX;

    void Start() {
        rb = GetComponent<Rigidbody2D>();

        //Sugeneruojamos atsitiktinės X ir Y reikšmės
        posY = Random.Range(1f, 9f);
        posX = Random.Range(15f, 25f);

        //Objektui suteikiama pozicija
        rb.transform.position = new Vector2(posX, posY);
    }
}