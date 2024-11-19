using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputControl : MonoBehaviour {
    //Sukuriami mygtuko, teksto komponentai
    public Button changeJumpButton;
    public TMP_Text jumpButtonText;

    //Pašokimo mygtuko kintamasis
    private KeyCode jumpKey = KeyCode.Space;

    //Kreipimasis į kintamąjį
    public KeyCode JumpKey => jumpKey;

    //Ar keičiamas mygtukas
    private bool isChangingKey = false;

    private void Start() {
        jumpKey = (KeyCode)LoadSettings();
        if (jumpKey == KeyCode.None) {
            jumpKey = KeyCode.Space;
        }
        //Atnaujinamas tekstas ir aprašomas įvykis
        UpdateJumpButtonText();
        changeJumpButton.onClick.AddListener(StartKeyChange);
    }

    private void Update() {
        //Tikrinama, ar keičiamas mygtukas
        if (isChangingKey) {
            //Jei taip, paleidžiama mygtuko priskyrimo funkcija
            DetectNewKey();
        }
    }

    //Pradedamas mygtuko keitimas
    void StartKeyChange() {
        isChangingKey = true;
    }

    //Priskiriamas naujas mygtukas
    void DetectNewKey() {
        //Randamas paspaustas naujas mygtukas
        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode))) {
            if (Input.GetKeyDown(keyCode)) {
                //Priskiriama reikšmė
                jumpKey = keyCode;

                //Atnaujinamas tekstas
                UpdateJumpButtonText();

                //Išsaugoma reikšmė
                SaveSettings(jumpKey);

                //Užbaigiamas mygtukko keitimas
                isChangingKey = false;
                return;
            }
        }
    }

    //Atnaujinamas mygtuko tekstas
    void UpdateJumpButtonText() {
        jumpButtonText.text = "(" + jumpKey + ")";
    }

    public void SaveSettings(KeyCode key) {
        PlayerPrefs.SetInt("jumpKey", (int)key);
    }

    public int LoadSettings() {
        return PlayerPrefs.GetInt("jumpKey");
    }
}