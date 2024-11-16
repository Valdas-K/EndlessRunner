using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    //Sukuriamas klasės objektas (singleton)
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    //Duomenų saugojimo klasės kintamasis
    public Data data;

    //Esamas rezultatas
    public float currentScore;
    public float highScore;
    public int collectedCoins;
    public int totalCoins;

    //Ar žaidžiama
    public bool isPlaying = false;

    //Sukuriami įvykiai pradedant ir baigiant žaidimą
    public UnityEvent onPlay = new();
    public UnityEvent onGameOver = new();
    public UnityEvent onCollectedCoin = new();

    private void Start()
    {
        //Įkeliami išsaugoti duomenys
        string loadedData = SaveSystem.Load("save");
        if (loadedData != null)
        {
            data = JsonUtility.FromJson<Data>(loadedData);
        } else
        {
            //Jei nėra duomenų, sukuriamas naujas duomenų kintamasis
            data = new Data();
            data.highscore = 0;
            data.coins = 0;
        }
    }

    private void Update()
    {
        //Jei žaidžiama, rezultatas didėja pagal išgyventą laiką
        if(isPlaying)
        {
            currentScore += Time.deltaTime;
        }
    }

    public void StartGame()
    {
        //Pradedamas žaidimas, pradinis rezultatas yra 0
        onPlay.Invoke();
        isPlaying = true;
        currentScore = 0;
        collectedCoins = 0;
        totalCoins = data.coins;
        highScore = data.highscore;
    }

    //Paėmus pinigą
    public void CoinCollected()
    {
        onCollectedCoin.Invoke();
        collectedCoins += 1;
    }

    //Pasibaigus žaidimui
    public void GameOver()
    {
        //Prie visų pinigų pridedami surinkti pinigai
        totalCoins += collectedCoins;

        //Duomenų kintamajam priskiriami visi pinigai
        data.coins = totalCoins;

        //Jei rezultatas yra aukščiausias, jis tampa geriausiu
        if (data.highscore < currentScore)
        {
            data.highscore = currentScore;
            highScore = currentScore;
        }

        //Išsaugomi surinkti pinigai ir geriausias rezultatas
        string saveString = JsonUtility.ToJson(data);
        SaveSystem.Save("save", saveString);


        //Baigiamas žaidimas
        onGameOver.Invoke();
        isPlaying = false;
    }
}