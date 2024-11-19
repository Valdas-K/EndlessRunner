using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputControl : MonoBehaviour {
    //Sukuriami mygtuko, teksto komponentai
    public Button changeJumpButton;
    public TMP_Text jumpButtonText;
    public Button changePauseButton;
    public TMP_Text pauseButtonText;

    //Pašokimo mygtuko kintamasis
    private KeyCode jumpKey = KeyCode.Space;
    private KeyCode pauseKey = KeyCode.Escape;

    //Kreipimasis į kintamąjį
    public KeyCode JumpKey => jumpKey;
    public KeyCode PauseKey => pauseKey;

    //Ar keičiamas pasokimo mygtukas
    private bool waitingForInput = false;
    private string actionToChange = "";

    private void Start() {
        LoadSettings();

        //Atnaujinamas tekstas ir aprašomas įvykis
        UpdateButtonText();
        changeJumpButton.onClick.AddListener(() => StartKeyChange("Jump"));
        changePauseButton.onClick.AddListener(() => StartKeyChange("Pause"));
    }

    private void Update() {
        //Tikrinama, ar keičiamas mygtukas
        if (waitingForInput) {
            //Jei taip, paleidžiama mygtuko priskyrimo funkcija
            DetectNewKey();
        }
    }

    //Pradedamas mygtuko keitimas
    void StartKeyChange(string action) {
        actionToChange = action;
        waitingForInput = true;
    }

    //Priskiriamas naujas mygtukas
    void DetectNewKey() {
        //Randamas paspaustas naujas mygtukas
        if (Input.anyKeyDown) {
            KeyCode key = GetPressedKey();
            if(key != KeyCode.None) {
                UpdateKey(key);
            }

            waitingForInput = false;
            SaveSettings();
            UpdateButtonText();
        }
    }

    public KeyCode GetPressedKey() {
        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode))) {
            if (Input.GetKeyDown(keyCode)) {
                return keyCode;
            }
        }
        return KeyCode.None;
    }

    public void UpdateKey(KeyCode newKey) {
        if(actionToChange == "Jump") {
            jumpKey = newKey;
        }
        if (actionToChange == "Pause") {
            pauseKey = newKey;
        }
    }

    //Atnaujinamas mygtuko tekstas
    void UpdateButtonText() {
        jumpButtonText.text = "(" + jumpKey + ")";
        pauseButtonText.text = "(" + pauseKey + ")";
    }

    public void SaveSettings() {
        PlayerPrefs.SetInt("JumpKey", (int)jumpKey);
        PlayerPrefs.SetInt("PauseKey", (int)pauseKey);

    }

    public void LoadSettings() {
        if (PlayerPrefs.HasKey("JumpKey")) {
            jumpKey = (KeyCode)PlayerPrefs.GetInt("JumpKey");
        } else if (jumpKey == KeyCode.None) {
            jumpKey = KeyCode.Space;
        }

        if (PlayerPrefs.HasKey("PauseKey")) {
            pauseKey = (KeyCode)PlayerPrefs.GetInt("PauseKey");
        }
        else if (pauseKey == KeyCode.None) {
            jumpKey = KeyCode.Escape;
        }
    }
}