using UnityEngine;

public class BackgroundScroller : MonoBehaviour {
    //Foninės nuotraukos komponentai
    [SerializeField] GameObject bg1;
    [SerializeField] GameObject bg2;
    
    //Fono judėjimo greitis
    [SerializeField] float scrollSpeed;

    //Vienos nuotraukos plotis (Nuotraukų pločiai turi būti vienodi)
    [SerializeField] float imageSize;

    //Pozicija, į kurią bus padėta fono nuotrauka
    private Vector2 resetPosition;

    private void Start() {
        //Fonui suteikiamas judėjimo greitis:
        //x reikšmei suteikiamas minusinis "scrollSpeed" kintamasis (tada fonas juda link žaidėjo),
        //y - 0, nes y ašyje fonas nejuda        
        bg1.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-scrollSpeed, 0);
        bg2.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-scrollSpeed, 0);

        //Naujai pozicijai suteikiama reikšmė:
        //x - vienos nuotraukos plotis, y - 2.5, nes nuotraukos yra šiame aukštyje
        resetPosition = new(imageSize, 2.5f);
    }

    private void Update() {
        //Suteikiamas besikartojančio fono efektas
        //Pasibaigusi nuotrauka perkeliama už sekančios
        if (bg1.transform.position.x < -imageSize) {
            ResetPicture(bg1);
        } else if (bg2.transform.position.x < -imageSize) {
            ResetPicture(bg2);
        }
    }

    private void ResetPicture(GameObject bg) {
        bg.transform.position = (Vector2)transform.position + resetPosition;
    }
}