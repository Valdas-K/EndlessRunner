using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {

    //Sukuriamas klasės objektas (singleton)
    public static GameManager Instance;

    [SerializeField] private AudioSource deathSound;

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
        //Nustatomi kintamieji pradedant žaidimą
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
        //Paleidžiamas pašokimo garso efektas
        coinsScore += 1;
    }

    //Paėmus pastiprinimą
    public void PowerCollected() {
        onCollectedPower.Invoke();
    }

    //Įveikus kliūtį
    public void EnemyDefeated() {
        onEnemyDefeated.Invoke();
        obstaclesScore += 1;
    }

    //Pasibaigus žaidimui
    public void GameOver() {
        deathSound.Play();


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

        //Kintamieji gauna reikšmes, kad vėliau neatsirastų klaidų
        Time.timeScale = 1;
        PauseGame.gameIsPaused = false;
        GameObject.FindWithTag("Player").transform.localScale = new Vector3(1f, 1f, 1f);
        
        //Baigiamas žaidimas
        onGameOver.Invoke();
        isPlaying = false;
    }

    //Išjungiamas žaidimas
    public void QuitGame()
    {
        Application.Quit();
    }
}