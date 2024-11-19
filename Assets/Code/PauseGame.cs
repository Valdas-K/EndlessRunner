using UnityEngine;

public class PauseGame : MonoBehaviour {

    //Sukuriamas klasės objektas
    GameManager gm;
    public static bool gameIsPaused;
    [SerializeField] private GameObject pauseMenu;

    //Aprašomas klasės kintamasis, iš kurio bus paimami valdymo nustatymai
    public InputControl inputcontrol;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start() {
        gm = GameManager.Instance;
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    private void Update() {
        //Jei žaidžiama, rezultatas didėja pagal išgyventą laiką
        if (gm.isPlaying == true) {

            if (Input.GetKeyDown(inputcontrol.PauseKey)) {
                gameIsPaused = !gameIsPaused;
                StopGame();
            }
        }
    }

    public void StopGame() {
        if (gameIsPaused) {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        } else {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }

    public void ChangeValues() {
        gameIsPaused = !gameIsPaused;
    }
}