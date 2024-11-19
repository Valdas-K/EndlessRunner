using UnityEngine;

public class PauseGame : MonoBehaviour
{

    //Sukuriamas klasės objektas
    GameManager gm;
    public static bool gameIsPaused;
    [SerializeField] private GameObject pauseMenu;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        gm = GameManager.Instance;
        pauseMenu.SetActive(false);

    }

    // Update is called once per frame
    private void Update()
    {
        //Jei žaidžiama, rezultatas didėja pagal išgyventą laiką
        if (GameManager.Instance.isPlaying == true)
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gameIsPaused = !gameIsPaused;
                StopGame();
            }

        
        }
    }

    public void StopGame()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }

    public void ChangeValues()
    {
        gameIsPaused = !gameIsPaused;
    }
}
