using UnityEngine;

public class PauseGame : MonoBehaviour {
    //Ar sustabdytas žaidimas
    public static bool gameIsPaused;

    //Pauzės meniu komponentas
    [SerializeField] GameObject pauseMenu;

    //Valdymo nustatymai
    public InputControl inputcontrol;

    private void Update() {
        //Jei žaidžiama ir yra paspaustas pauzės mygtukas, žaidimas sustoja
        if (GameManager.Instance.isPlaying && Input.GetKeyDown(inputcontrol.PauseKey)) {
            StopGame();
        }
    }

    //Žaidimas yra sustabdomas ir atvaizduojamas pauzės meniu
    public void StopGame() {
        //Pakeičiamos reikšmės
        gameIsPaused = !gameIsPaused;
        if (gameIsPaused) {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            AudioListener.pause = true;
        } else {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
            AudioListener.pause = false;
        }
    }
}