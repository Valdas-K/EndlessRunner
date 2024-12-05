using UnityEngine;

public class ButtonController : MonoBehaviour {
    //Mygtuko paspaudimo garsas
    [SerializeField] AudioSource clickSound;

    //Paspaustas bet kuris mygtukas
    public void ClickButton() {
        clickSound.Play();
    }
}