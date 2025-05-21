using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//LJ - Long Jump (Default), DJ - Double Jump (Frog), TP - Third Player

public class SwitchPlayer : MonoBehaviour {
    //Veikėjų objektai
    public GameObject longJumpBody, doubleJumpBody, thirdPlayerBody;

    //Kuris veikėjas pasirinktas
    public int playerPicked;

    //Veikėjo kaina
    [SerializeField] int price;
    [SerializeField] int TPprice;

    //Vartotojo sąsajos komponentai
    [SerializeField] MenuController mc;
    [SerializeField] public TextMeshProUGUI djButtonText;
    [SerializeField] public TextMeshProUGUI tpButtonText;

    public GameManager gm;

    [SerializeField] Button ljButton;
    [SerializeField] Button djButton;
    [SerializeField] Button tpButton;

    void Start() {
        //Užkraunamas išsaugotas veikėjo pasirinkimas
        gm = GameManager.Instance;
        LoadSettings();
    }

    //Užkraunamas išsaugotas veikėjas
    public void LoadSettings() {
        playerPicked = PlayerPrefs.GetInt("PlayerPicked");
        if (playerPicked == 1 && gm.data.frogBodyOwned == true) {
            ChangePlayer(1);
        } else if (playerPicked == 2 && gm.data.thirdPlayerBodyOwned == true) {
            ChangePlayer(2);
        }
        else {
            ChangePlayer(0);
        }
    }

    private void SaveSettings() {
        PlayerPrefs.SetInt("PlayerPicked", playerPicked);
        PlayerPrefs.Save();
        mc.UpdateCoinsUI();
    }

    //Pasirenkamas reikiamas veikėjas, aktyvuojamas veikėjo objektas, išsaugomas pasirinkimas
    public void ChangePlayer(int player) {
        playerPicked = player;
        longJumpBody.SetActive(false);
        doubleJumpBody.SetActive(false);
        thirdPlayerBody.SetActive(false);
        if (playerPicked == 1) {
            doubleJumpBody.SetActive(true);
            doubleJumpBody.transform.position = new Vector3(-5f, 6f, 0f);
        } else if (playerPicked == 2) {
            thirdPlayerBody.SetActive(true);
            thirdPlayerBody.transform.position = new Vector3(-5f, 6f, 0f);
        } else {
            longJumpBody.SetActive(true);
            longJumpBody.transform.position = new Vector3(-5f, 6f, 0f);
        }
        SaveSettings();
        mc.ClickMainButton();
    }

    public void SelectLongJump() {
        ChangePlayer(0);
    }

    //Perkamas antras veikėjas
    async public void BuyFrogBody() {
        //Tikrinama, ar veikėjas jau nėra nupirktas
        if (gm.data.frogBodyOwned == false) {
            //Jei nėra, tikrinama, ar užtenka pinigų nupirkti veikėją
            if (gm.data.coins >= price) {
                //Jei užtenka, veikėja nuperkamas, užkraunamas ir išsaugomas
                gm.data.coins -= price;
                gm.totalCoins = gm.data.coins;
                gm.data.frogBodyOwned = true;
                gm.SaveData();
                ChangePlayer(1);
            }
            else {
                //Jei neužtenka pinigų, porai sekundžių įjungiamas pagalbinis tekstas
                djButtonText.text = "Not Enough Coins";
                await Task.Delay(2000);
                djButtonText.text = "Price: 10C";
            }
        } else {
            //Jei yra, jis užkraunamas
            ChangePlayer(1);
        }
    }

    async public void BuyThirdPlayerBody() {
        //Tikrinama, ar veikėjas jau nėra nupirktas
        if (gm.data.thirdPlayerBodyOwned == false) {
            //Jei nėra, tikrinama, ar užtenka pinigų nupirkti veikėją
            if (gm.data.coins >= TPprice) {
                //Jei užtenka, veikėja nuperkamas, užkraunamas ir išsaugomas
                gm.data.coins -= TPprice;
                gm.totalCoins = gm.data.coins;
                gm.data.thirdPlayerBodyOwned = true;
                gm.SaveData();
                ChangePlayer(2);
            }
            else {
                //Jei neužtenka pinigų, porai sekundžių įjungiamas pagalbinis tekstas
                tpButtonText.text = "Not Enough Coins";
                await Task.Delay(2000);
                tpButtonText.text = "Price: 50C";
            }
        } else {
            //Jei yra, jis užkraunamas
            ChangePlayer(2);
        }
    }
}