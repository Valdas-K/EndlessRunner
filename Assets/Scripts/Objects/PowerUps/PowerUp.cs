using UnityEngine;

public abstract class PowerUp : MonoBehaviour {
    //Pastiprinimo modifikacijos reikšmė, veikimo laikas ir garso efektas
    [SerializeField] protected float multiplier;
    [SerializeField] protected float duration;
    public AudioSource sound;

    protected void OnTriggerEnter2D(Collider2D other) {
        //Palietus žaidėją, paleidžiamas metodas
        if (other.CompareTag("Player")) {
            StartLogic(other);
        }
    }

    protected abstract void StartLogic(Collider2D player);
    protected void ChangeFactors() {
        //Išjungiami pastiprinimo komponentai, kad jų nematytų žaidėjas ir būtų geresnė vartotojo patirtis
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }
}