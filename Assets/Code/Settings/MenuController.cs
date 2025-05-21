using System.Collections;
using UnityEngine;
using System.Diagnostics;
using TMPro;

public class MenuController : MonoBehaviour {
    //Aprašomi lygių ir meniu konteineriai, meniu pakeitimo laikas, ekrano plotis ir aukštis
    [SerializeField] RectTransform menuContainer;
    [SerializeField] float transitionTime;
    [SerializeField] GameManager gm;
    [SerializeField] MusicController mc;
    [SerializeField] GameObject menuWindows;
    [SerializeField] TextMeshProUGUI coinsUI;
    [SerializeField] FirebaseManager firebaseManager;
    [SerializeField] GameObject profileMenu;
    [SerializeField] GameObject loginMenu;
    [SerializeField] GameObject registerMenu;
    [SerializeField] TextMeshProUGUI helpText;
    [SerializeField] Leaderboards board;
    [SerializeField] SwitchPlayer shop;

    private enum MenuType {
        //Aprašomi meniu langai
        Main, LevelSelect, Shop, Settings, Profile, Leaderboards
    }

    private void ChangeMenu(MenuType menuType) {
        //Patikrinamas norimo meniu tipas ir gaunama reikiama pozicija
        Vector3 newPosition;
        if (menuType == MenuType.LevelSelect) {
            newPosition = new Vector3(-3900f, 0f, 0f);
        } else if (menuType == MenuType.Shop) {
            newPosition = new Vector3(3900f, 0f, 0f);
        } else if (menuType == MenuType.Settings) {
            newPosition = new Vector3(0f, 2200f, 0f);
        } else if (menuType == MenuType.Profile) {
            newPosition = new Vector3(0f, -2200f, 0f);
        } else if (menuType == MenuType.Leaderboards) {
            newPosition = new Vector3(-7800f, 0f, 0f);
        }
        else {
            newPosition = Vector3.zero;
        }

        //Sustabdomos visos korutinos siekiant išvengti nenumatytų atvejų ir kitų klaidų
        StopAllCoroutines();

        //Paleidžiama meniu perjungimo korutina su reikiamo meniu pozicija
        StartCoroutine(MenuTransition(newPosition));
    }

    private IEnumerator MenuTransition(Vector3 newPosition) {
        //Meniu perjungimas, suteikiami kintamieji praėjusiam laikui ir senai meniu pozicijai
        Vector3 oldPosition = menuContainer.anchoredPosition3D;

        //Kaupiamas laikas, atnaujinama pozicija
        for (float elapsedTime = 0f; elapsedTime <= transitionTime; elapsedTime += Time.deltaTime) {
            Vector3 currentPosition = Vector3.Lerp(oldPosition, newPosition, elapsedTime / transitionTime);
            menuContainer.anchoredPosition3D = currentPosition;
            yield return null;
        }
    }

    public void ClickLevelSelectButton() {
        //Lygių pasirinkimo mygtukas
        ChangeMenu(MenuType.LevelSelect);
    }

    public void ClickLeaderboardsButton() {
        //Geriausiu rezultatu ekrano mygtukas
        if (firebaseManager.isLoggedIn) {
            StartCoroutine(board.LoadScoreboardData());
            //yield return new WaitForSecondsRealtime(1f);

            ChangeMenu(MenuType.Leaderboards);
            helpText.text = string.Empty;
        } else {
            helpText.text = "Only logged in users can view leaderboards";
        }
    }

    public void ClickProfileButton() {
        //Profilio mygtukas
        if (firebaseManager.isLoggedIn) {
            loginMenu.SetActive(false);
            profileMenu.SetActive(true);
        } else {
            profileMenu.SetActive(false);
            loginMenu.SetActive(true);
        }
        ChangeMenu(MenuType.Profile);
    }

    public void ClickShopButton() {
        //Parduotuvės mygtukas
        if (gm.data.frogBodyOwned) {
            shop.djButtonText.text = "Select";
        } else {
            shop.djButtonText.text = "Price: 10C";
        } 
        if (gm.data.thirdPlayerBodyOwned) {
            shop.tpButtonText.text = "Select";
        } else {
            shop.tpButtonText.text = "Price: 50C";   
        }
        UpdateCoinsUI();
        ChangeMenu(MenuType.Shop);
    }

    public void ClickSettingsButton() {
        //Nustatymų mygtukas
        ChangeMenu(MenuType.Settings);
    }

    public void ClickMainButton() {
        //Pradinio meniu mygtukas
        gm.SaveData();
        if (firebaseManager.isLoggedIn) {
            firebaseManager.SaveDataButton();
        }
        ChangeMenu(MenuType.Main);
    }

    public void ClickQuitButton() {
        //Žaidimo pabaigos mygtukas
        gm.SaveData();
        Application.Quit();
        Process.GetCurrentProcess().Kill();
    }

    public void ClickStartButton() {
        //Žaidimo pradžios mygtukas
        gm.StartGame();
    }

    //Paslepiami visi meniu
    public void HideAllMenus() {
        menuWindows.SetActive(false);
    }

    public void ShowAllMenus() {
        menuWindows.SetActive(true);
        UpdateCoinsUI();
    }

    public void UpdateCoinsUI() {
        coinsUI.text = "Coins: " + gm.data.coins;
    }

    public void OpenRegisterMenu() {
        loginMenu.SetActive(false);
        profileMenu.SetActive(false);
        registerMenu.SetActive(true);
    }

    public void OpenLoginMenu() {
        profileMenu.SetActive(false);
        registerMenu.SetActive(false);
        loginMenu.SetActive(true);
    }

    public void ResetProfileMenu() {
        profileMenu.SetActive(false);
        loginMenu.SetActive(false);
        registerMenu.SetActive(false);
    }
}