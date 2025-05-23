using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Localization.Settings;
using System.Diagnostics;

public class MenuController : MonoBehaviour {
    //Aprašomi meniu konteineriai, meniu pakeitimo laikas ir vartotojo sąsajos elementai
    [SerializeField] RectTransform menuContainer;
    [SerializeField] float transitionTime;
    [SerializeField] GameManager gm;
    [SerializeField] GameObject menuWindows;
    [SerializeField] TextMeshProUGUI coinsUI;
    [SerializeField] FirebaseManager firebaseManager;
    [SerializeField] GameObject profileMenu;
    [SerializeField] GameObject loginMenu;
    [SerializeField] GameObject registerMenu;
    [SerializeField] TextMeshProUGUI helpText;
    [SerializeField] Leaderboards board;
    [SerializeField] SwitchPlayer shop;
    [SerializeField] SettingsController settings;
    private float elapsedTime = 0f;

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
        } else {
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
        for (elapsedTime = 0f; elapsedTime <= transitionTime; elapsedTime += Time.deltaTime) {
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
        //Geriausių rezultatų ekrano mygtukas
        if (firebaseManager.isLoggedIn) {
            //Į meniu galima patekti tik prisijungus
            StartCoroutine(board.LoadScoreboardData());
            ChangeMenu(MenuType.Leaderboards);
            helpText.gameObject.SetActive(false);
        } else {
            helpText.gameObject.SetActive(true);
        }
    }

    public void ClickProfileButton() {
        //Profilio mygtukas, patikrinama, ar vartotojas prisijungęs ir atidaromas reikiamas meniu
        if (firebaseManager.isLoggedIn) {
            OpenProfileMenu();
        } else {
            OpenLoginMenu();
        }
        ChangeMenu(MenuType.Profile);
    }

    public void ClickShopButton() {
        //Parduotuvės mygtukas, patikrinama, ar yra atrakinti veikėjai ir atnaujinamas tekstas
        if (LocalizationSettings.SelectedLocale.ToString() == "Lithuanian (lt)") {
            if (gm.data.frogBodyOwned) {
                shop.djButtonText.text = "Pasirinkti";
            }
            if (gm.data.thirdPlayerBodyOwned) {
                shop.tpButtonText.text = "Pasirinkti";
            }
        } else {
            if (gm.data.frogBodyOwned) {
                shop.djButtonText.text = "Select";
            }
            if (gm.data.thirdPlayerBodyOwned) {
                shop.tpButtonText.text = "Select";
            }
        }
        if(gm.data.frogBodyOwned == false) {
            shop.djButtonText.text = "10C";
        }
        if (gm.data.thirdPlayerBodyOwned == false) {
            shop.tpButtonText.text = "50C";
        }
        UpdateCoinsUI();
        ChangeMenu(MenuType.Shop);
    }

    public void ClickSettingsButton() {
        //Nustatymų mygtukas
        ChangeMenu(MenuType.Settings);
    }

    public void ClickMainButton() {
        //Pradinio meniu mygtukas, kurį paspaudus yra išsaugomi visi rezultatai
        gm.SaveData();
        settings.SaveSettings();
        if (firebaseManager.isLoggedIn) {
            firebaseManager.SaveDataButton();
        }
        ChangeMenu(MenuType.Main);
    }

    public void ClickQuitButton() {
        //Žaidimo pabaigos mygtukas, sustabdomos korutinos, baigiamos meniu tranzicijos ir išsaugomi duomenys
        gm.SaveData();
        StopAllCoroutines();
        elapsedTime = transitionTime;
        Application.Quit();
        Process.GetCurrentProcess().Kill();
    }

    public void ClickStartButton() {
        //Žaidimo pradžios mygtukas, paleidžiamas pasirinktas lygis
        gm.StartGame();
    }

    public void HideAllMenus() {
        //Paslepiami visi meniu
        menuWindows.SetActive(false);
    }

    public void ShowAllMenus() {
        //Atvaizduojami visi meniu
        menuWindows.SetActive(true);
        UpdateCoinsUI();
    }

    public void UpdateCoinsUI() {
        //Atnaujinamas parduotuvės lange esantis pinigų teksto laukas
        coinsUI.text = gm.data.coins.ToString();
    }

    public void OpenRegisterMenu() {
        //Atidaromas registracijos meniu
        loginMenu.SetActive(false);
        profileMenu.SetActive(false);
        registerMenu.SetActive(true);
    }

    public void OpenLoginMenu() {
        //Atidaromas prisijungimo meniu
        profileMenu.SetActive(false);
        registerMenu.SetActive(false);
        loginMenu.SetActive(true);
    }

    public void OpenProfileMenu() {
        //Atidaromas profilio meniu
        profileMenu.SetActive(true);
        registerMenu.SetActive(false);
        loginMenu.SetActive(false);
    }
}