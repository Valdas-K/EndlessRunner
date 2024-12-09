using UnityEngine;

public class ButtonController : MonoBehaviour {
    [SerializeField] MusicController mc;

    //Meniu komponentai
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject profileMenu;
    [SerializeField] GameObject registerMenu;
    [SerializeField] GameObject levelSelectMenu;
    [SerializeField] GameObject shopMenu;
    [SerializeField] GameObject leaderboardsMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject controlsMenu;
    [SerializeField] GameObject creditsMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameOverMenu;

    //Paslepiami visi meniu
    public void HideMenus() {
        mc.ClickButton();
        startMenu.SetActive(false);
        profileMenu.SetActive(false);
        registerMenu.SetActive(false);
        levelSelectMenu.SetActive(false);
        shopMenu.SetActive(false);
        leaderboardsMenu.SetActive(false);
        settingsMenu.SetActive(false);
        controlsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
    }

    //Išjungiamas žaidimas
    public void QuitGame() {
        mc.ClickButton();
        Application.Quit();
    }
}