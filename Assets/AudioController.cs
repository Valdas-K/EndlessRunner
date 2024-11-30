using UnityEngine;

public class AudioController : MonoBehaviour
{

    [SerializeField] private AudioSource menuMusic;
    [SerializeField] private AudioSource gameMusic;
    public GameManager gm;

    private void Start()
    {
        gm = GameManager.Instance;
    }
    private void Update()
    {
        if (gm.isPlaying) {
            menuMusic.volume = -80;

            gameMusic.volume = 20;
        } else {
            gameMusic.volume = -80;

            menuMusic.volume = 20;
        }
    }
}
