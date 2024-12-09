using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputControl : MonoBehaviour {
    //Sukuriami mygtuko, teksto komponentai
    public Button changeJumpButton;
    public TMP_Text jumpButtonText;
    public Button changePauseButton;
    public TMP_Text pauseButtonText;

    //Pašokimo ir žaidimo sustabdymo mygtukų kintamieji
    private KeyCode jumpKey = KeyCode.Space;
    private KeyCode pauseKey = KeyCode.Escape;

    //Kreipimasis į kintamuosius
    public KeyCode JumpKey => jumpKey;
    public KeyCode PauseKey => pauseKey;

    //Ar keičiamas mygtukas
    private bool waitingForInput = false;

    //Kuris veiksmas atliekamas
    private string actionToChange = "";

    private void Start() {
        //Užkraunami nustatymai
        LoadSettings();

        //Atnaujinamas mygtukų tekstas ir aprašomi įvykiai
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
            //Jei naujas mygtukas nėra priskirtas kitam veiksmui, jis atnaujinamas
            if (key != KeyCode.None && key != JumpKey && key != PauseKey) {
                UpdateKey(key);
            }
            //Išsaugomi nustatymai ir atnaujinamas tekstas
            waitingForInput = false;
            SaveSettings();
            UpdateButtonText();
        }
    }

    //Grąžinamas paspaustas mygtukas
    public KeyCode GetPressedKey() {
        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode))) {
            if (Input.GetKeyDown(keyCode)) {
                return keyCode;
            }
        }
        return KeyCode.None;
    }

    //Atnaujinamas mygtukas pagal veiksmą
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

    //Išsaugomi nustatymai, kurie bus išsaugomi ir perkrovus žaidimą
    public void SaveSettings() {
        PlayerPrefs.SetInt("JumpKey", (int)jumpKey);
        PlayerPrefs.SetInt("PauseKey", (int)pauseKey);

    }

    //Užkraunami nustatymai kiekvienam mygtukui
    //Jei nėra išsaugotas joks mygtukas, yra priskiriama nutylėta reikšmė
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