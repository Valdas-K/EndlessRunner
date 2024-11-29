using UnityEngine;

public class ButtonSound : MonoBehaviour {
    [SerializeField] private AudioSource clickSound;

    public void ClickButton() {
        clickSound.Play();
    }
}