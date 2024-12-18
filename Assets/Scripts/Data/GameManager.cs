using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {

    //Sukuriamas klasės objektas (singleton)
    public static GameManager Instance;

    //Valdymo nustatymai
    [SerializeField] MusicController mc;
    [SerializeField] InputControl input;
    [SerializeField] PauseGame pause;
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
            if (Input.GetKeyDown(input.PauseKey)) {
                pause.StopGame();
            }
        }
    }

    public void LoadData() {
        //Įkeliami išsaugoti duomenys
        Data loadedData = SaveSystem.Load("save");
        if (loadedData != null) {
            data = loadedData;
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
        SaveData();
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
            playerBody[i].transform.position = new Vector3(-5f, 6f, 0f);
            if (playerBody[i].GetComponent<LongPlayerMovement>() != null) {
                playerBody[i].GetComponent<LongPlayerMovement>().jumpForce = 13f;
            }
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

        if (timeScore < 0) {
            gameScore = 0;
        }

        //Jei rezultatas yra aukščiausias, jis tampa geriausiu
        if (data.highscore < gameScore) {
            data.highscore = gameScore;
            highScore = gameScore;
        }

        SaveData();

        isPlaying = false;

        //Baigiamas žaidimas
        onGameOver.Invoke();
    }

    //Išjungiamas žaidimas
    public void QuitGame() {
        SaveData();
        mc.ClickButton();
        Application.Quit();
    }

    public void SaveData() {
        //Išsaugomi surinkti pinigai ir geriausias rezultatas
        //string saveString = JsonUtility.ToJson(data);
        SaveSystem.Save("save", data);
    }
}