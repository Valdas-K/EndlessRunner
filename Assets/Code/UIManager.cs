using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    //Sukuriami vartotojo sąsajos elementai
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private GameObject startMenuUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TextMeshProUGUI gameOverScoreUI;
    [SerializeField] private TextMeshProUGUI gameOverHighscoreUI;
    [SerializeField] private TextMeshProUGUI gameOverCoinsCollectedUI;
    [SerializeField] private TextMeshProUGUI gameOverTotalCoinsUI;

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
        scoreUI.text = "Score: " + gm.currentScore.ToString("F0") + "\nCoins: " + gm.collectedCoins;
    }

    public void ActivateGameOverUI()
    {
        //Pasibaigus žaidimui, paleidžiamas meniu ir parodomi rezultatai
        gameOverUI.SetActive(true);
        gameOverScoreUI.text = "Score: " + gm.currentScore.ToString("F0");
        gameOverHighscoreUI.text = "Highscore: " + gm.highScore.ToString("F0");
        gameOverCoinsCollectedUI.text = "Coins Collected: " + gm.collectedCoins.ToString();
        gameOverTotalCoinsUI.text = "Total Coins: " + gm.totalCoins.ToString();
    }

    //Paspaudus pradėti žaidimą mygtuką, žaidimas prasideda
    public void PlayButtonHandler()
    {
        gm.StartGame();
    }
}
