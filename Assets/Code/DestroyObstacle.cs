using UnityEngine;

public class DestroyObstacle : MonoBehaviour
{
    private void Update()
    {
        //Jei nėra paleistas lygis, visos kliūtys sunaikinamos
        if (!GameManager.Instance.isPlaying)
        {
            Destroy(gameObject);
        }
    }

    //Kliūtys sunaikinamos kai paliečia nematomą ribą
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Destroy"))
        {
            Destroy(gameObject);
        }
    }
}
