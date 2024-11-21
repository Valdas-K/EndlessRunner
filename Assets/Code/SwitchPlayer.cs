using TMPro;
using UnityEngine;

//LJ - Long Jump (Default)
//DJ - Double Jump (Frog)

public class SwitchPlayer : MonoBehaviour {
    public GameObject longJumpBody, doubleJumpBody;
    public int price;
    public TextMeshProUGUI coinsUI;
    public TextMeshProUGUI dJButtonText;
    public TextMeshProUGUI djHintText;
    public int playerPicked;
    public Rigidbody2D rb;

    //Duomenų saugojimo klasės kintamasis
    public Data data;
    public GameManager gm;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        LoadPlayer();
        gm = GameManager.Instance;
        coinsUI.text = "Total Coins: " + gm.data.coins;

        if (gm.data.ownedCharacters == "DJ")
        {
            dJButtonText.text = "Select";
        }
    }

    private void LoadPlayer() {
        playerPicked = PlayerPrefs.GetInt("PlayerPicked");
        if (playerPicked == 1 && gm.data.ownedCharacters == "DJ"){
            LoadDJ();
        } else {
            LoadLJ();
        }
    }

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

    public void SwitchDJBody() {
        if (gm.data.ownedCharacters != "DJ") {
            if (gm.data.coins > price) {
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
                djHintText.enabled = true;
                Invoke(nameof(HideText), 2f);
            }
        } else {
            HideText();
            LoadDJ();
        }
    }

    public void HideText()
    {
        djHintText.enabled = false;
    }
}