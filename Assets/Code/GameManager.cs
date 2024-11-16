using System;
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

    //Esamas rezultatas distancija
    public int coinsScore;
    public int enemiesScore;
    public float distanceScore;

    public float gameScore;
    public float highScore;
    public int totalCoins;


    //Ar žaidžiama
    public bool isPlaying = false;

    //Sukuriami įvykiai pradedant ir baigiant žaidimą
    public UnityEvent onPlay = new();
    public UnityEvent onGameOver = new();
    public UnityEvent onCollectedCoin = new();
    public UnityEvent onEnemyDefeated = new();

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
            data = new Data
            {
                highscore = 0,
                coins = 0
            };
        }
    }

    private void Update()
    {
        //Jei žaidžiama, rezultatas didėja pagal išgyventą laiką
        if(isPlaying)
        {
            distanceScore += Time.deltaTime;
        }
    }

    public void StartGame()
    {
        //Pradedamas žaidimas, pradinis rezultatas yra 0
        onPlay.Invoke();
        isPlaying = true;
        distanceScore = 0;
        coinsScore = 0;
        enemiesScore = 0;
        gameScore = 0;
        totalCoins = data.coins;
        highScore = data.highscore;
    }

    //Paėmus pinigą
    public void CoinCollected()
    {
        onCollectedCoin.Invoke();
        coinsScore += 1;
    }

    //Paėmus pinigą
    public void EnemyDefeated()
    {
        onEnemyDefeated.Invoke();
        enemiesScore += 1;
    }

    //Pasibaigus žaidimui
    public void GameOver()
    {
        gameScore = coinsScore + enemiesScore + MathF.Ceiling(distanceScore);

        //Prie visų pinigų pridedami surinkti pinigai
        totalCoins += coinsScore;

        //Duomenų kintamajam priskiriami visi pinigai
        data.coins = totalCoins;

        //Jei rezultatas yra aukščiausias, jis tampa geriausiu
        if (data.highscore < gameScore)
        {
            data.highscore = gameScore;
            highScore = gameScore;
        }

        //Išsaugomi surinkti pinigai ir geriausias rezultatas
        string saveString = JsonUtility.ToJson(data);
        SaveSystem.Save("save", saveString);


        //Baigiamas žaidimas
        onGameOver.Invoke();
        isPlaying = false;
    }
}