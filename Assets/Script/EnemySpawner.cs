using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Masukkan Prefab Enemy ke slot ini di Inspector nanti
    public GameObject enemyPrefab; 

    // Waktu jeda antar spawn (dalam detik)
    public float spawnInterval = 3f; 
    private float timer;

    // Titik lokasi spawn (bisa diisi beberapa titik di map)
    public Transform[] spawnPoints; 

    void Start()
    {
        timer = spawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnEnemy();
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
}