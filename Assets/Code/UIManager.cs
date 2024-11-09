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
        scoreUI.text = gm.PrettyScore("1");
    }

    public void ActivateGameOverUI()
    {
        //Pasibaigus žaidimui, paleidžiamas meniu ir parodomi rezultatai
        gameOverUI.SetActive(true);
        gameOverScoreUI.text = "Score: " + gm.PrettyScore("1");
        gameOverHighscoreUI.text = "Highscore: " + gm.PrettyScore("2");
    }

    //Paspaudus pradėti žaidimą mygtuką, žaidimas prasideda
    public void PlayButtonHandler()
    {
        gm.StartGame();
    }
}
