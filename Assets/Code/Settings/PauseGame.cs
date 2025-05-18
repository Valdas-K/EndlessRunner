using UnityEngine;

public class PauseGame : MonoBehaviour {
    //Ar sustabdytas žaidimas
    public static bool gameIsPaused;

    //Pauzės meniu komponentas
    [SerializeField] GameObject pauseMenu;

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