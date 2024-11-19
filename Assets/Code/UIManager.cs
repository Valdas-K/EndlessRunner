using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    //Sukuriami vartotojo sąsajos elementai
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private TextMeshProUGUI coinsUI;
    [SerializeField] private GameObject startMenuUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TextMeshProUGUI gameOverScoreUI;
    [SerializeField] private TextMeshProUGUI gameOverHighscoreUI;
    [SerializeField] private TextMeshProUGUI gameOverCoinsCollectedUI;
    [SerializeField] private TextMeshProUGUI gameOverTotalCoinsUI;
    [SerializeField] private TextMeshProUGUI gameOverObstaclesUI;
    [SerializeField] private TextMeshProUGUI gameOverDistanceUI;




    //Sukuriamas klasės objektas
    GameManager gm;

    private void Start()
    {
        gm = GameManager.Instance;

        //Pasibaigus žaidimui, paleidžiamas atitinkamas meniu
        gm.onGameOver.AddListener(ActivateGameOverUI);
    }

    private void OnGUI()
    {
        //Rezultatas suapvalinamas
        scoreUI.text = "Distance: " + gm.distanceScore.ToString("F0") + "\nCoins: " + gm.coinsScore + "\nEnemies: " + gm.enemiesScore;
        coinsUI.text = "Total Coins: " + gm.totalCoins.ToString();
    }

    public void ActivateGameOverUI()
    {
        //Pasibaigus žaidimui, paleidžiamas meniu ir parodomi rezultatai
        gameOverUI.SetActive(true);

        gameOverDistanceUI.text = "Distance: " + gm.distanceScore.ToString("F0");
        gameOverObstaclesUI.text = "Obstacles: " + gm.enemiesScore.ToString();
        gameOverCoinsCollectedUI.text = "Coins: " + gm.coinsScore.ToString();
        gameOverScoreUI.text = "Game Score: " + gm.gameScore.ToString();

        gameOverHighscoreUI.text = "Highscore: " + gm.highScore.ToString("F0");
        gameOverTotalCoinsUI.text = "Total Coins: " + gm.totalCoins.ToString();

    }

    //Paspaudus pradėti žaidimą mygtuką, žaidimas prasideda
    public void PlayButtonHandler()
    {
        gm.StartGame();
    }
}
