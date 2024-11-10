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
        scoreUI.text = "Score: " + gm.currentScore.ToString("F0");
    }

    public void ActivateGameOverUI()
    {
        //Pasibaigus žaidimui, paleidžiamas meniu ir parodomi rezultatai
        gameOverUI.SetActive(true);
        gameOverScoreUI.text = "Score: " + gm.currentScore.ToString("F0");
        gameOverHighscoreUI.text = "Highscore: " + gm.data.highscore.ToString("F0");
    }

    //Paspaudus pradėti žaidimą mygtuką, žaidimas prasideda
    public void PlayButtonHandler()
    {
        gm.StartGame();
    }
}
