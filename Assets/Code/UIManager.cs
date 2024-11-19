using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour {
    //Sukuriami vartotojo sąsajos elementai
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private TextMeshProUGUI coinsUI;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private TextMeshProUGUI gameOverTimeScore;
    [SerializeField] private TextMeshProUGUI gameOverObstacleScore;
    [SerializeField] private TextMeshProUGUI gameOverCoinsScore;
    [SerializeField] private TextMeshProUGUI gameOverTotalScore;
    [SerializeField] private TextMeshProUGUI gameOverHighScore;
    [SerializeField] private TextMeshProUGUI gameOverTotalCoins;

    //Sukuriamas klasės objektas
    GameManager gm;

    private void Start() {
        gm = GameManager.Instance;

        //Pasibaigus žaidimui, paleidžiamas atitinkamas meniu
        gm.onGameOver.AddListener(ActivateGameOverUI);
    }

    private void OnGUI() {
        //Rezultatas suapvalinamas
        scoreUI.text = "Time: " + gm.timeScore.ToString("F0") + "\nCoins: " + gm.coinsScore + "\nEnemies: " + gm.obstaclesScore;
        coinsUI.text = "Total Coins: " + gm.totalCoins.ToString();
    }

    public void ActivateGameOverUI() {
        scoreUI.enabled = false;
        //Pasibaigus žaidimui, paleidžiamas meniu ir parodomi rezultatai
        gameOverMenu.SetActive(true);

        gameOverTimeScore.text = "Time: " + gm.timeScore.ToString("F0");
        gameOverObstacleScore.text = "Obstacles: " + gm.obstaclesScore.ToString();
        gameOverCoinsScore.text = "Coins: " + gm.coinsScore.ToString();
        gameOverTotalScore.text = "Game Score: " + gm.gameScore.ToString();
        gameOverHighScore.text = "Highscore: " + gm.highScore.ToString("F0");
        gameOverTotalCoins.text = "Total Coins: " + gm.totalCoins.ToString();

    }

    //Paspaudus pradėti žaidimą mygtuką, žaidimas prasideda
    public void PlayButtonHandler() {
        scoreUI.enabled = true;
        gm.StartGame();
    }
}