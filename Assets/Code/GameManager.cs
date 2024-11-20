using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {

    //Sukuriamas klasės objektas (singleton)
    public static GameManager Instance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    //Duomenų saugojimo klasės kintamasis
    public Data data;

    //Esamas rezultatas distancija
    public int coinsScore;
    public int obstaclesScore;
    public float timeScore;

    public float gameScore;
    public float highScore;
    public int totalCoins;

    public float powers = 0;

    //Ar žaidžiama
    public bool isPlaying = false;

    //Sukuriami įvykiai pradedant ir baigiant žaidimą
    public UnityEvent onPlay = new();
    public UnityEvent onGameOver = new();
    public UnityEvent onCollectedCoin = new();
    public UnityEvent onEnemyDefeated = new();
    public UnityEvent onCollectedPower = new();

    private void Start() {
        //Įkeliami išsaugoti duomenys
        string loadedData = SaveSystem.Load("save");
        if (loadedData != null) {
            data = JsonUtility.FromJson<Data>(loadedData);
        } else {
            //Jei nėra duomenų, sukuriamas naujas duomenų kintamasis
            data = new Data {
                highscore = 0,
                coins = 0
            };
        }
        totalCoins = data.coins;
        highScore = data.highscore;
    }

    private void Update() {
        //Jei žaidžiama, rezultatas didėja pagal išgyventą laiką
        if(isPlaying) {
            timeScore += Time.deltaTime;
        }    
    }

    public void StartGame() {
        //Pradedamas žaidimas, pradinis rezultatas yra 0
        onPlay.Invoke();
        isPlaying = true;
        timeScore = 0;
        coinsScore = 0;
        obstaclesScore = 0;
        gameScore = 0;
        totalCoins = data.coins;
        highScore = data.highscore;
    }

    //Paėmus pinigą
    public void CoinCollected() {
        onCollectedCoin.Invoke();
        coinsScore += 1;
    }

    //Paėmus pinigą
    public void PowerCollected() {
        onCollectedPower.Invoke();
        powers += 1;
        Debug.Log(powers);

    }

    //Paėmus pinigą
    public void EnemyDefeated() {
        onEnemyDefeated.Invoke();
        obstaclesScore += 1;
    }

    //Pasibaigus žaidimui
    public void GameOver() {
        timeScore = Mathf.RoundToInt(timeScore);
        gameScore = coinsScore + obstaclesScore + timeScore;

        //Prie visų pinigų pridedami surinkti pinigai
        totalCoins += coinsScore;

        //Jei rezultatas yra aukščiausias, jis tampa geriausiu
        if (data.highscore < gameScore) {
            data.highscore = gameScore;
            highScore = gameScore;
        }

        //Duomenų kintamajam priskiriami visi pinigai
        data.coins = totalCoins;

        //Išsaugomi surinkti pinigai ir geriausias rezultatas
        string saveString = JsonUtility.ToJson(data);
        SaveSystem.Save("save", saveString);

        Time.timeScale = 1;
        PauseGame.gameIsPaused = false;

        //Baigiamas žaidimas
        onGameOver.Invoke();
        isPlaying = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}