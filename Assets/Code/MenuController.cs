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

    private int screenWidth;
    private int screenHeight;

    private enum MenuType {
        //Aprašomi meniu langai
        Main, LevelSelect, Shop, Settings, Profile
    }

    private void Start() {
        //Užkrovus žaidimą, yra priskiriamos reikšmės
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    private void ChangeMenu(MenuType menuType) {
        //Patikrinamas norimo meniu tipas ir gaunama reikiama pozicija
        Vector3 newPosition;
        if (menuType == MenuType.LevelSelect) {
            newPosition = new Vector3(-screenWidth, 0f, 0f);
        } else if (menuType == MenuType.Shop) {
            newPosition = new Vector3(screenWidth, 0f, 0f);
        } else if (menuType == MenuType.Settings) {
            newPosition = new Vector3(0f, screenHeight, 0f);
        } else if (menuType == MenuType.Profile) {
            newPosition = new Vector3(0f, -screenHeight, 0f);
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

    public void ClickProfileButton() {
        //Profilio mygtukas
        ChangeMenu(MenuType.Profile);
    }

    public void ClickShopButton() {
        //Parduotuvės mygtukas
        ChangeMenu(MenuType.Shop);
    }

    public void ClickSettingsButton() {
        //Nustatymų mygtukas
        ChangeMenu(MenuType.Settings);
    }

    public void ClickMainButton() {
        //Pradinio meniu mygtukas
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
}