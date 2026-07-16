using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; 

    [SerializeField] private float spawnInterval = 3f; 
    private float timer;
    [SerializeField] private int maxEnemies = 10;

    [SerializeField] private Transform[] spawnPoints; 

    void Start()
    {
        timer = spawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            if (GetCurrentEnemyCount() < maxEnemies)
            {
                SpawnEnemy();
            }
            timer = spawnInterval; // Reset timer
        }
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0) return;

        // Pilih titik spawn secara acak dari list spawnPoints
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform selectedPoint = spawnPoints[randomIndex];

        // Gandakan prefab enemy di posisi yang terpilih
        Instantiate(enemyPrefab, selectedPoint.position, selectedPoint.rotation);
    }

    int GetCurrentEnemyCount()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length;
    }
}