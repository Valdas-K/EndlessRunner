using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour {

    //Meniu komponentai
    [SerializeField] TextMeshProUGUI scoreUI;

    [SerializeField] TextMeshProUGUI gameOverTimeScore;
    [SerializeField] TextMeshProUGUI gameOverObstacleScore;
    [SerializeField] TextMeshProUGUI gameOverCoinsScore;
    [SerializeField] TextMeshProUGUI gameOverTotalScore;
    [SerializeField] TextMeshProUGUI gameOverHighScore;
    [SerializeField] TextMeshProUGUI gameOverTotalCoins;

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameOverMenu;

    GameManager gm;
    [SerializeField] MusicController mc;

    private void Start() {
        gm = GameManager.Instance;

        //Pasibaigus žaidimui, paleidžiamas atitinkamas meniu
        gm.onGameOver.AddListener(ActivateGameOverUI);
    }

    private void OnGUI() {
        //Atvaizduojami rezultatai
        scoreUI.text = "Time: " + gm.timeScore.ToString("F0") + "\nCoins: " + gm.coinsScore + "\nObstacles: " + gm.obstaclesScore;
    }

    //Pasibaigus žaidimui, paleidžiamas meniu ir parodomi rezultatai
    public void ActivateGameOverUI() {
        scoreUI.enabled = false;
        gameOverMenu.SetActive(true);
        gameOverTimeScore.text = "Time: " + gm.timeScore.ToString("F0");
        gameOverObstacleScore.text = "Obstacles: " + gm.obstaclesScore.ToString();
        gameOverCoinsScore.text = "Coins: " + gm.coinsScore.ToString();
        gameOverTotalScore.text = "Game Score: " + gm.gameScore.ToString();
        gameOverHighScore.text = "Highscore: " + gm.highScore.ToString("F0");
        gameOverTotalCoins.text = "Total Coins: " + gm.totalCoins.ToString();
    }
}