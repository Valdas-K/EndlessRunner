using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputSettings : MonoBehaviour {
    //Sukuriami mygtuko, teksto komponentai
    public Button changeJumpButton;
    public TMP_Text jumpButtonText;
    public Button changePauseButton;
    public TMP_Text pauseButtonText;

    //Pašokimo ir žaidimo sustabdymo mygtukų kintamieji
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode pauseKey = KeyCode.Escape;

    //Ar keičiamas mygtukas
    private bool waitingForInput = false;

    //Kuris veiksmas atliekamas
    private string actionToChange = "";

    private void Start() {
        //Atnaujinamas mygtukų tekstas ir aprašomi įvykiai
        UpdateButtonText();
    }

    private void Update() {
        //Tikrinama, ar keičiamas mygtukas
        if (waitingForInput) {
            //Jei taip, paleidžiama mygtuko priskyrimo funkcija
            DetectNewKey();
        }
    }

    //Pradedamas mygtuko keitimas
    public void StartKeyChange(string action) {
        actionToChange = action;
        waitingForInput = true;
    }

    //Priskiriamas naujas mygtukas
    private void DetectNewKey() {
        //Randamas paspaustas naujas mygtukas
        if (Input.anyKeyDown) {
            KeyCode key = GetPressedKey();
            //Jei naujas mygtukas nėra priskirtas kitam veiksmui, jis atnaujinamas
            if (key != KeyCode.None && key != jumpKey && key != pauseKey) {
                UpdateKey(key);
            }
            //Išsaugomi nustatymai ir atnaujinamas tekstas
            waitingForInput = false;
            SaveSettings();
            UpdateButtonText();
        }
    }

    //Grąžinamas paspaustas mygtukas
    private KeyCode GetPressedKey() {
        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode))) {
            if (Input.GetKeyDown(keyCode)) {
                return keyCode;
            }
        }
        return KeyCode.None;
    }

    //Atnaujinamas mygtukas pagal veiksmą
    private void UpdateKey(KeyCode newKey) {
        if(actionToChange == "Jump") {
            jumpKey = newKey;
        }
        if (actionToChange == "Pause") {
            pauseKey = newKey;
        }
    }

    //Atnaujinamas mygtuko tekstas
    private void UpdateButtonText() {
        jumpButtonText.text = "(" + jumpKey + ")";
        pauseButtonText.text = "(" + pauseKey + ")";
    }

    //Išsaugomi nustatymai, kurie bus išsaugomi ir perkrovus žaidimą
    private void SaveSettings() {
        PlayerPrefs.SetInt("JumpKey", (int)jumpKey);
        PlayerPrefs.SetInt("PauseKey", (int)pauseKey);
    }
}