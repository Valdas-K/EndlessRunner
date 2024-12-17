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

    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject profileMenu;
    [SerializeField] GameObject registerMenu;
    [SerializeField] GameObject loginMenu;
    [SerializeField] GameObject levelSelectMenu;
    [SerializeField] GameObject shopMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject controlsMenu;
    [SerializeField] GameObject creditsMenu;
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
        scoreUI.text = "Time: " + gm.timeScore.ToString("F0") + "\nCoins: " + gm.coinsScore + "\nEnemies: " + gm.obstaclesScore;
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

    //Paspaudus pradėti žaidimą mygtuką, žaidimas prasideda
    public void PlayButtonHandler() {
        scoreUI.enabled = true;
        gm.StartGame();
    }

    //Paslepiami visi meniu
    public void HideMenus() {
        mc.ClickButton();
        startMenu.SetActive(false);
        profileMenu.SetActive(false);
        registerMenu.SetActive(false);
        levelSelectMenu.SetActive(false);
        shopMenu.SetActive(false);
        settingsMenu.SetActive(false);
        controlsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        loginMenu.SetActive(false);
    }

    public void OpenStartMenu() {
        HideMenus();
        startMenu.SetActive(true);
    }

    public void OpenProfileMenu() {
        HideMenus();
        profileMenu.SetActive(true);
    }

    public void OpenRegisterMenu() {
        HideMenus();
        registerMenu.SetActive(true);
    }

    public void OpenLevelSelectMenu() {
        HideMenus();
        levelSelectMenu.SetActive(true);
    }

    public void OpenShopMenu() {
        HideMenus();
        shopMenu.SetActive(true);
    }

    public void OpenSettingsMenu() {
        HideMenus();
        settingsMenu.SetActive(true);
    }

    public void OpenControlsMenu() {
        HideMenus();
        controlsMenu.SetActive(true);
    }

    public void OpenCreditsMenu() {
        HideMenus();
        creditsMenu.SetActive(true);
    }

    public void OpenPauseMenu() {
        HideMenus();
        pauseMenu.SetActive(true);
    }

    public void OpenGameOverMenu() {
        HideMenus();
        gameOverMenu.SetActive(true);
    }

    public void OpenLoginMenu() {
        HideMenus();
        loginMenu.SetActive(true);
    }
}