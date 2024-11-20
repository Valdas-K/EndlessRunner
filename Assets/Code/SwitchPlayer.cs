using TMPro;
using UnityEngine;

public class SwitchPlayer : MonoBehaviour {
    public GameObject longJumpBody, doubleJumpBody;
    public int price = 10;
    bool isOwned = false;
    public TextMeshProUGUI coinsUI;
    public TextMeshProUGUI dJButtonText;
    public TextMeshProUGUI djHintText;
    private int playerPicked = 1;

    //Duomenų saugojimo klasės kintamasis
    public Data data;
    public GameManager gm;

    void Start() {
        LoadPlayer();
        gm = GameManager.Instance;
    }

    private void LoadPlayer() {
        playerPicked = PlayerPrefs.GetInt("PlayerPicked");
        if (playerPicked == 0) {
            LoadLJ();
        } else if (playerPicked == 0){
            //ckeck if isowned
            LoadDJ();
        }
    }

    private void LoadLJ() {
        longJumpBody.SetActive(true);
        doubleJumpBody.SetActive(false);
        djHintText.text = "";
    }

    private void LoadDJ() {
        doubleJumpBody.SetActive(true);
        longJumpBody.SetActive(false);
        djHintText.text = "";
    }

    public void SwitchLJBody() {
        playerPicked = 1;
        LoadLJ();
        PlayerPrefs.SetInt("PlayerPicked", playerPicked);
    }

    public void SwitchDJBody() {
        if (isOwned == false) {
            if (gm.data.coins > price) {
                gm.data.coins -= price;
                Debug.Log(gm.data.coins);
                dJButtonText.text = "Select";
                playerPicked = 2; 
                PlayerPrefs.SetInt("PlayerPicked", playerPicked);
                LoadDJ();

                //Išsaugomi surinkti pinigai ir geriausias rezultatas
                string saveString = JsonUtility.ToJson(gm.data);
                SaveSystem.Save("save", saveString);

                isOwned = true;
                djHintText.text = "";
                //not working
                coinsUI.text = "Total Coins: " + gm.data.coins;
            }
            else {
                djHintText.text = "Not Enough Coins!";
            }
        } else {
            djHintText.text = "";
            dJButtonText.text = "Select";
            LoadDJ();
        }
    }
}