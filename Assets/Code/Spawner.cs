using UnityEngine;

public class Spawner : MonoBehaviour
{
    //Visų kliūčių masyvas
    [SerializeField] private GameObject[] spawnObjectPrefabs;

    //Pradinis kliūčių atsiradimo laikas ir greitis
    public float startingObstacleSpawnTime;
    public float startingObstacleSpeed;

    //Kintamieji, pagal kuriuos bus apskaičiuojamas žaidimo sunkumas
    [Range(0, 1)] public float obstacleSpawnTimeFactor;
    [Range(0, 1)] public float obstacleSpeedFactor;

    //Kliūčių atsiradimo laikas ir greitis
    private float obstacleSpawnTime;
    private float obstacleSpeed;

    //Išgyventas laikas
    private float timeAlive;

    //Laikas iki kitos kliūties
    private float timeUntilObstacleSpawn;

    private void Start()
    {
        //Pradedant žaidimą, atstatomos pradinės reikšmės
        GameManager.Instance.onPlay.AddListener(ResetFactors);
    }
    private void Update()
    {
        if(GameManager.Instance.isPlaying)
        {
            //Jei yra žaidžiama, yra skaičiuojamas išgyventas laikas, apskaičiuojamas tolesnis žaidimo sunkumas ir generuojamos kliūtys
            timeAlive += Time.deltaTime;
            CalculateFactors();
            SpawnLoop();
        }
    }

    private void CalculateFactors()
    {
        //Apskaičiuojamas kliūčių atsiradimo greitis ir kliūčių greitis
        obstacleSpawnTime = startingObstacleSpawnTime / Mathf.Pow(timeAlive, obstacleSpawnTimeFactor);
        obstacleSpeed = startingObstacleSpeed * Mathf.Pow(timeAlive, obstacleSpeedFactor);
    }

    private void SpawnLoop()
    {
        //Kaupiamas laikis iki sekančios kliūties
        timeUntilObstacleSpawn += Time.deltaTime;

        if(timeUntilObstacleSpawn >= obstacleSpawnTime)
        {
            //Jei sukauptas laikas didesnis už kliūties atsiradimo laiką, generuojama nauja kliūtis ir atnaujinamas sukauptas laikas
            Spawn();
            timeUntilObstacleSpawn = 0f;
        }
    }

    private void ResetFactors()
    {
        //Atstatomi kintamieji į pradines reikšmes
        timeAlive = 1f;
        obstacleSpawnTime = startingObstacleSpawnTime;
        obstacleSpeed = startingObstacleSpeed;
    }

    private void Spawn()
    {
        if(spawnObjectPrefabs.Length > 0)
        {
            //Parenkama kliūtis
            GameObject obstacleToSpawn = spawnObjectPrefabs[Random.Range(0, spawnObjectPrefabs.Length)];

            //Kliūtis sugeneruojama
            GameObject spawnedObstacle = Instantiate(obstacleToSpawn, transform.position, Quaternion.identity);

            //Parenkamas greitis
            Rigidbody2D obstacleRB = spawnedObstacle.GetComponent<Rigidbody2D>();
            obstacleRB.linearVelocity = Vector2.left * obstacleSpeed;
        }
    }
}