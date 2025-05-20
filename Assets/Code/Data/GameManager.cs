using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {

    //Sukuriamas klasės objektas (singleton)
    public static GameManager Instance;

    [SerializeField] TextMeshProUGUI scoreUI;

    //Valdymo nustatymai
    [SerializeField] MusicController mc;
    [SerializeField] InputSettings input;
    [SerializeField] PauseGame pause;
    [SerializeField] Collider2D[] playerBody;
    public Data data;

    [SerializeField] TextMeshProUGUI gameOverTimeScore;
    [SerializeField] TextMeshProUGUI gameOverObstacleScore;
    [SerializeField] TextMeshProUGUI gameOverCoinsScore;
    [SerializeField] TextMeshProUGUI gameOverTotalScore;
    [SerializeField] TextMeshProUGUI gameOverHighScore;
    [SerializeField] TextMeshProUGUI gameOverTotalCoins;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameOverMenu;
    public int chosenLevel;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    //Rezultatai: pinigų, kliučių, laiko, bendras, geriausias ir visi surinkti pinigai
    public int coinsScore;
    public int obstaclesScore;
    public float timeScore;
    public int gameScore;
    public float highScore1;
    public float highScore2;
    public float highScore3;
    public int totalCoins;
    public bool frogBodyOwned;
    public bool thirdPlayerBodyOwned;

    //Ar žaidžiama
    public bool isPlaying = false;

    //Įvykiai žaidimo pradžiai, pabaigai, surinkus pinigą, pastiprinimą ir įveikus kliūtį
    public UnityEvent onGameOver = new();

    private void Start() {
        LoadData();
        mc.StartMenuMusic();
        //Pasibaigus žaidimui, paleidžiamas atitinkamas meniu
        onGameOver.AddListener(ActivateGameOverUI);
    }

    private void Update() {
        //Jei žaidžiama, rezultatas didėja pagal išgyventą laiką
        if (isPlaying) {
            timeScore += Time.deltaTime;
            if (Input.GetKeyDown(input.pauseKey)) {
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
                level1 = 0,
                level2 = 0,
                level3 = 0,
                coins = 0,
                frogBodyOwned = false,
                thirdPlayerBodyOwned = false,
            };
        }
        totalCoins = data.coins;
        highScore1 = data.level1;
        highScore2 = data.level2;
        highScore3 = data.level3;
        frogBodyOwned = data.frogBodyOwned;
        thirdPlayerBodyOwned = data.thirdPlayerBodyOwned;
        chosenLevel = PlayerPrefs.GetInt("lastLevel");
    }

    public void StartGame() {
        //Nustatomi kintamieji pradedant žaidimą
        SaveData();
        scoreUI.enabled = true;
        isPlaying = true;
        totalCoins = data.coins;
        highScore1 = data.level1;
        highScore2 = data.level2;
        highScore3 = data.level3;
        //frogBodyOwned = data.frogBodyOwned;
        //thirdPlayerBodyOwned = data.thirdPlayerBodyOwned;
        ResetScores();
        mc.StartGameMusic();
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
        gameScore = coinsScore + obstaclesScore + Mathf.RoundToInt(timeScore);
        if (timeScore < 0) {
            gameScore = 0;
        }

        //Duomenų kintamajam priskiriami visi pinigai
        totalCoins += coinsScore;
        data.coins = totalCoins;

        //Jei rezultatas yra aukščiausias, jis tampa geriausiu
        if (chosenLevel == 0) {
            if (data.level1 < gameScore) {
                data.level1 = gameScore;
                highScore1 = gameScore;
            }
        } else if (chosenLevel == 1) {
            if (data.level2 < gameScore) {
                data.level2 = gameScore;
                highScore2 = gameScore;
            }
        } else {
            if (data.level3 < gameScore) {
                data.level3 = gameScore;
                highScore3 = gameScore;
            }
        }

        SaveData();
        isPlaying = false;

        //Baigiamas žaidimas
        onGameOver.Invoke();
    }

    public void SaveData() {
        //Išsaugomi surinkti pinigai ir geriausias rezultatas
        SaveSystem.Save("save", data);
    }

    private void OnGUI() {
        //Atvaizduojami rezultatai
        scoreUI.text = "Time: " + timeScore.ToString("F0") + "\nCoins: " + coinsScore + "\nObstacles: " + obstaclesScore;
    }

    //Pasibaigus žaidimui, paleidžiamas meniu ir parodomi rezultatai
    public void ActivateGameOverUI() {
        scoreUI.enabled = false;
        gameOverMenu.SetActive(true);
        gameOverTimeScore.text = "Time: " + timeScore.ToString("F0");
        gameOverObstacleScore.text = "Obstacles: " + obstaclesScore.ToString();
        gameOverCoinsScore.text = "Coins: " + coinsScore.ToString();
        gameOverTotalScore.text = "Game Score: " + gameScore.ToString();
        gameOverTotalCoins.text = "Total Coins: " + totalCoins.ToString();

        if (chosenLevel == 0) {
            gameOverHighScore.text = "Highscore: " + highScore1.ToString("F0");
        } else if (chosenLevel == 1) {
            gameOverHighScore.text = "Highscore: " + highScore2.ToString("F0");
        } else {
            gameOverHighScore.text = "Highscore: " + highScore3.ToString("F0");
        }
    }
}