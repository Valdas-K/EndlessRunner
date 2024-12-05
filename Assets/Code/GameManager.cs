using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {

    //Sukuriamas klasės objektas (singleton)
    public static GameManager Instance;

    [SerializeField] private AudioSource deathSound;

    [SerializeField] private AudioSource menuMusic;
    [SerializeField] private AudioSource gameMusic;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    //Aprašomas duomenų saugojimo klasės kintamasis
    public Data data;

    //Aprašomi rezultatai: pinigų, kliučių, laiko, bendras, geriausias ir visi surinkti pinigai
    public int coinsScore;
    public int obstaclesScore;
    public float timeScore;
    public float gameScore;
    public float highScore;
    public int totalCoins;

    //Ar žaidžiama
    public bool isPlaying = false;

    //Sukuriami įvykiai žaidimo pradžiai, pabaigai, surinkus pinigą, pastiprinimą ir įveikus kliūtį
    public UnityEvent onPlay = new();
    public UnityEvent onGameOver = new();
    public UnityEvent onCollectedCoin = new();
    public UnityEvent onEnemyDefeated = new();

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

        StartMenuMusic();
    }

    private void Update() {
        //Jei žaidžiama, rezultatas didėja pagal išgyventą laiką
        if(isPlaying) {
            timeScore += Time.deltaTime;
        }    
    }

    public void StartGame() {
        //Nustatomi kintamieji pradedant žaidimą
        onPlay.Invoke();
        isPlaying = true;
        timeScore = 0;
        coinsScore = 0;
        obstaclesScore = 0;
        gameScore = 0;
        totalCoins = data.coins;
        highScore = data.highscore;
        StartGameMusic();
        Time.timeScale = 1.0f;
    }

    //Paėmus pinigą
    public void CoinCollected() {
        onCollectedCoin.Invoke();
        coinsScore += 1;
    }

    //Įveikus kliūtį
    public void EnemyDefeated() {
        onEnemyDefeated.Invoke();
        obstaclesScore += 1;
    }

    //Pasibaigus žaidimui
    public void GameOver() {
        gameMusic.Stop();

        //Kintamieji gauna reikšmes, kad vėliau neatsirastų klaidų
        Time.timeScale = 1.0f;

        //Apskaičiuojamas bendras rezultatas
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

        deathSound.Play();
        isPlaying = false;

        //Baigiamas žaidimas
        onGameOver.Invoke();
    }

    //Išjungiamas žaidimas
    public void QuitGame() {
        Application.Quit();
    }

    //Paleidžiama meniu muzika
    public void StartMenuMusic() {
        gameMusic.Stop();
        menuMusic.time = Random.Range(0f, menuMusic.clip.length);
        menuMusic.Play();
    }

    //Paleidžiama lygio muzika
    public void StartGameMusic() {
        menuMusic.Stop();
        gameMusic.time = Random.Range(0f, gameMusic.clip.length);
        gameMusic.Play();
    }
}