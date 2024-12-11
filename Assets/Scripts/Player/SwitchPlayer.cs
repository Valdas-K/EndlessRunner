using TMPro;
using UnityEngine;

//LJ - Long Jump (Default)
//DJ - Double Jump (Frog)

public class SwitchPlayer : MonoBehaviour {
    //Veikėjų objektai
    public GameObject longJumpBody, doubleJumpBody;

    //Kuris veikėjas pasirinktas
    public int playerPicked;

    //Veikėjo kaina
    [SerializeField] int price;

    //Vartotojo sąsajos komponentai
    [SerializeField] TextMeshProUGUI djButtonText;
    [SerializeField] UIManager UI;
    public GameManager gm;

    private string hintText = "Buy";

    void Start() {
        //Užkraunamas išsaugotas veikėjo pasirinkimas
        gm = GameManager.Instance;
        LoadSettings();
    }

    //Užkraunamas išsaugotas veikėjas
    public void LoadSettings() {
        playerPicked = PlayerPrefs.GetInt("PlayerPicked");
        if (playerPicked == 1 && gm.data.ownedCharacters == "DJ") {
            hintText = "Select";
            LoadDJ();
        } else if (playerPicked == 0) {
            if (gm.data.ownedCharacters == "DJ") {
                hintText = "Select";
            } else {
                hintText = "Buy";
            }
            LoadLJ();
        } else {
            LoadLJ();
        }
        ChangeHintText();
    }

    private void SaveSettings() {
        PlayerPrefs.SetInt("PlayerPicked", playerPicked);
        PlayerPrefs.Save();
        ChangeHintText();
    }

    //Pasirenkamas pirmas veikėjas, pasirinkto žaidėjo kintamieji
    //Aktyvuojamas veikėjo objektas, išsaugomas pasirinkimas
    public void LoadLJ() {
        playerPicked = 0;
        longJumpBody.SetActive(true);
        doubleJumpBody.SetActive(false);
        SaveSettings();
        longJumpBody.transform.localPosition = new Vector3(0f, 1f, 0f);
    }

    //Pasirenkamas antras veikėjas (jei nupirktas), pasirinkto žaidėjo kintamieji
    //Aktyvuojamas veikėjo objektas, išsaugomas pasirinkimas
    public void LoadDJ() {
        playerPicked = 1;
        doubleJumpBody.SetActive(true);
        longJumpBody.SetActive(false);
        SaveSettings();
        doubleJumpBody.transform.localPosition = new Vector3(0f, 1.5f, 0f);
    }

    //Perkamas antras veikėjas
    public void BuyFrogBody() {
        //Tikrinama, ar veikėjas jau nėra nupirktas
        if (gm.data.ownedCharacters != "DJ") {
            //Jei nėra, tikrinama, ar užtenka pinigų nupirkti veikėją
            if (gm.data.coins > price) {
                //Jei užtenka, veikėja nuperkamas, užkraunamas ir išsaugomas
                gm.data.coins -= price;
                gm.totalCoins = gm.data.coins;
                gm.data.ownedCharacters = "DJ";
                string saveString = JsonUtility.ToJson(gm.data);
                SaveSystem.Save("save", saveString);
                hintText = "Select";
                ChangeHintText();
                LoadDJ();
            }
            else {
                //Jei neužtenka pinigų, porai sekundžių įjungiamas pagalbinis tekstas
                hintText = "More";
                ChangeHintText();

                hintText = "Buy";
                Invoke(nameof(ChangeHintText), 2f);
            }
        } else {
            //Jei yra, jis užkraunamas
            LoadDJ();
        }
    }

    public void ChangeHintText() {
        string option = hintText;
        djButtonText.text = option switch
        {
            "Select" => "Select",
            "Buy" => "Price: 10C",
            "More" => "Not Enough Coins",
            _ => "Price: 10C",
        };
    }
}