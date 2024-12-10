using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {

    //Sukuriamas klasės objektas (singleton)
    public static GameManager Instance;

    [SerializeField] MusicController mc;
    [SerializeField] Collider2D[] playerBody;
    public Data data;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    //Rezultatai: pinigų, kliučių, laiko, bendras, geriausias ir visi surinkti pinigai
    public int coinsScore;
    public int obstaclesScore;
    public float timeScore;
    public float gameScore;
    public float highScore;
    public int totalCoins;
    public string ownedCharacters;

    //Ar žaidžiama
    public bool isPlaying = false;

    //Įvykiai žaidimo pradžiai, pabaigai, surinkus pinigą, pastiprinimą ir įveikus kliūtį
    public UnityEvent onPlay = new();
    public UnityEvent onGameOver = new();

    private void Start() {
        LoadData();
        mc.StartMenuMusic();
    }

    private void Update() {
        //Jei žaidžiama, rezultatas didėja pagal išgyventą laiką
        if (isPlaying) {
            timeScore += Time.deltaTime;
        }
    }

    public void LoadData() {
        //Įkeliami išsaugoti duomenys
        string loadedData = SaveSystem.Load("save");
        if (loadedData != null) {
            data = JsonUtility.FromJson<Data>(loadedData);
        } else {
            //Jei nėra duomenų, sukuriamas naujas duomenų kintamasis
            data = new Data {
                highscore = 0,
                coins = 0,
                ownedCharacters = ""
            };
        }
        totalCoins = data.coins;
        highScore = data.highscore;
        ownedCharacters = data.ownedCharacters;
    }

    public void StartGame() {
        //Nustatomi kintamieji pradedant žaidimą
        isPlaying = true;
        totalCoins = data.coins;
        highScore = data.highscore;
        ownedCharacters = data.ownedCharacters;
        ResetScores();
        mc.StartGameMusic();
        onPlay.Invoke();
    }

    public void ResetScores() {
        timeScore = 0;
        coinsScore = 0;
        obstaclesScore = 0;
        gameScore = 0;
    }

    //Paėmus pinigą
    public void CoinCollected() {
        mc.PlayCoinSound();
        coinsScore += 1;
    }

    //Įveikus kliūtį
    public void EnemyDefeated() {
        obstaclesScore += 1;
    }

    //Pasibaigus žaidimui
    public void GameOver() {
        //Veikėjų dydžiai ir laikas atstatomi į pradines padėtis, kad vėliau neatsirastų klaidų
        Time.timeScale = 1.0f;
        for (int i = 0; i < playerBody.Length; i++) {
            playerBody[i].transform.localScale = Vector3.one;
        }

        //Sustabdoma muzika ir paleidžiamas žaidimo pabaigos garsas
        mc.StopAllMusic();
        mc.PlayDeathSound();

        //Apskaičiuojamas bendras rezultatas
        timeScore = Mathf.RoundToInt(timeScore);
        gameScore = coinsScore + obstaclesScore + timeScore;

        //Duomenų kintamajam priskiriami visi pinigai
        totalCoins += coinsScore;
        data.coins = totalCoins;

        //Jei rezultatas yra aukščiausias, jis tampa geriausiu
        if (data.highscore < gameScore) {
            data.highscore = gameScore;
            highScore = gameScore;
        }

        //Išsaugomi surinkti pinigai ir geriausias rezultatas
        string saveString = JsonUtility.ToJson(data);
        SaveSystem.Save("save", saveString);

        isPlaying = false;

        //Baigiamas žaidimas
        onGameOver.Invoke();
    }
}