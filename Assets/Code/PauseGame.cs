using UnityEngine;

public class PauseGame : MonoBehaviour {
    //Sukuriamas klasės objektas
    GameManager gm;

    //Ar sustabdytas žaidimas
    public static bool gameIsPaused;

    //Pauzės meniu komponentas
    [SerializeField] private GameObject pauseMenu;

    //Aprašomas klasės kintamasis, iš kurio bus paimami valdymo nustatymai
    public InputControl inputcontrol;

    //Paslepiamas meniu
    private void Start() {
        gm = GameManager.Instance;
        pauseMenu.SetActive(false);
    }

    private void Update() {
        //Jei žaidžiama ir yra paspaustas pauzės mygtukas, žaidimas sustoja
        if (gm.isPlaying == true && Input.GetKeyDown(inputcontrol.PauseKey)) {
            gameIsPaused = !gameIsPaused;
            StopGame();
        }
    }

    //Žaidimas yra sustabdomas ir atvaizduojamas pauzės meniu
    public void StopGame() {
        if (gameIsPaused) {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        } else {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }

    //Pakeičiamos reikšmės
    public void ChangeValues() {
        gameIsPaused = !gameIsPaused;
    }
}