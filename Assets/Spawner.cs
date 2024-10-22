using System.IO;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private Transform obstacleParent;
    public float startingObstacleSpawnTime = 3f;
    public float startingObstacleSpeed = 4f;

    [Range(0, 1)] public float obstacleSpawnTimeFactor = 0.1f;
    [Range(0, 1)] public float obstacleSpeedFactor = 0.2f;

    private float obstacleSpawnTime;
    private float obstacleSpeed;

    private float timeAlive;


    private float timeUntilObstacleSpawn = 2f;

    private void Start()
    {
        GameManager.Instance.onGameOver.AddListener(ClearObstacles);
        GameManager.Instance.onPlay.AddListener(ResetFactors);
    }
    private void Update()
    {
        if(GameManager.Instance.isPlaying)
        {
            timeAlive += Time.deltaTime;

            CalculateFactors();

            SpawnLoop();
        }
    }

    private void CalculateFactors()
    {
        obstacleSpawnTime = startingObstacleSpawnTime / Mathf.Pow(timeAlive, obstacleSpawnTimeFactor);
        obstacleSpeed = startingObstacleSpeed * Mathf.Pow(timeAlive, obstacleSpeedFactor);
    }

    private void SpawnLoop()
    {
        timeUntilObstacleSpawn += Time.deltaTime;
        if(timeUntilObstacleSpawn >= obstacleSpawnTime)
        {
            Spawn();
            timeUntilObstacleSpawn = 0f;
        }
    }

    private void ResetFactors()
    {
        timeAlive = 1f;
        obstacleSpawnTime = startingObstacleSpawnTime;
        obstacleSpeed = startingObstacleSpeed;
    }

    private void ClearObstacles()
    {
        foreach(Transform child in obstacleParent)
        {
            Destroy(child.gameObject);
        }
    }

    private void Spawn()
    {
        GameObject obstacleToSpawn = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        GameObject spawnedObstacle = Instantiate(obstacleToSpawn, transform.position, Quaternion.identity);
        spawnedObstacle.transform.parent = obstacleParent;
        Rigidbody2D obstacleRB = spawnedObstacle.GetComponent<Rigidbody2D>();
        obstacleRB.linearVelocity = Vector2.left * obstacleSpeed;
    }
}
