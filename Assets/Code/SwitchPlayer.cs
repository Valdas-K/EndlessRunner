using TMPro;
using UnityEngine;

//LJ - Long Jump (Default)
//DJ - Double Jump (Frog)

public class SwitchPlayer : MonoBehaviour {
    //Aprašomi veikėjų objektai
    public GameObject longJumpBody, doubleJumpBody;

    //Kaina
    public int price;

    //Vartotojo sąsajos komponentai
    public TextMeshProUGUI coinsUI;
    public TextMeshProUGUI dJButtonText;
    public TextMeshProUGUI djHintText;

    //Kuris veikėjas pasirinktas
    public int playerPicked;
    public Rigidbody2D rb;

    //Duomenų saugojimo klasės kintamasis
    public Data data;
    public GameManager gm;

    void Start() {
        rb = GetComponent<Rigidbody2D>();

        //Žaidimo pradžioje užkraunamas išsaugotas pasirinktas veikėjas
        LoadPlayer();
        gm = GameManager.Instance;

        //Pakeičiami UI elementai
        coinsUI.text = "Total Coins: " + gm.data.coins;
        if (gm.data.ownedCharacters == "DJ") {
            dJButtonText.text = "Select";
        }
    }

    //Užkraunamas išsaugotas veikėjas
    private void LoadPlayer() {
        playerPicked = PlayerPrefs.GetInt("PlayerPicked");
        if (playerPicked == 1 && gm.data.ownedCharacters == "DJ"){
            LoadDJ();
        } else {
            LoadLJ();
        }
    }

    //Pasirenkamas pirmas veikėjas, aprašomi masės, pasirinkto žaidėjo kintamieji
    //Aktyvuojamas veikėjo objektas, išsaugomas pasirinkimas
    public void LoadLJ() {
        rb.mass = 0.1f;
        playerPicked = 0;
        longJumpBody.SetActive(true);
        doubleJumpBody.SetActive(false);
        HideText();
        PlayerPrefs.SetInt("PlayerPicked", playerPicked);
        PlayerPrefs.Save();
        coinsUI.text = "Total Coins: " + gm.totalCoins;
    }

    //Pasirenkamas antras veikėjas (jei nupirktas), aprašomi masės, pasirinkto žaidėjo kintamieji
    //Aktyvuojamas veikėjo objektas, išsaugomas pasirinkimas
    public void LoadDJ() {
        rb.mass = 0.3f;
        playerPicked = 1;
        doubleJumpBody.SetActive(true);
        longJumpBody.SetActive(false);
        HideText();
        PlayerPrefs.SetInt("PlayerPicked", playerPicked);
        PlayerPrefs.Save();
        coinsUI.text = "Total Coins: " + gm.totalCoins;
    }


    //Perkamas antras veikėjas
    public void SwitchDJBody() {
        //Tikrinama, ar veikėjas jau nėra nupirktas
        if (gm.data.ownedCharacters != "DJ") {
            //Jei nėra, tikrinama, ar užtenka pinigų nupirkti veikėją
            if (gm.data.coins > price) {
                //Jei užtenka, veikėja nuperkamas, užkraunamas ir išsaugomas
                gm.data.coins -= price;
                gm.totalCoins = gm.data.coins;
                LoadDJ();

                gm.data.ownedCharacters = "DJ";
                string saveString = JsonUtility.ToJson(gm.data);
                SaveSystem.Save("save", saveString);

                dJButtonText.text = "Select";
                HideText();

                coinsUI.text = "Total Coins: " + gm.totalCoins;
            }
            else {
                //Jei neužtenka pinigų, porai sekundžių įjungiamas pagalbinis tekstas
                djHintText.enabled = true;
                Invoke(nameof(HideText), 2f);
            }
        } else {
            //Jei yra, jis užkraunamas
            HideText();
            LoadDJ();
        }
    }

    //Paslepiamas papildomas tekstas
    public void HideText()
    {
        djHintText.enabled = false;
    }
}