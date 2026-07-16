using UnityEngine;

public class BuffSpawner : MonoBehaviour
{
    [Header("Buff Prefabs")]
    public GameObject[] buffPrefabs; // Masukkan prefab Heal dan Shield ke sini

    [Header("Spawn Settings")]
    public float spawnInterval = 7f; // Muncul setiap 7 detik
    private float timer;

    [Header("Spawn Area Boundaries")]
    [Tooltip("Batas minimal dan maksimal koordinat X di map")]
    public float minX;
    public float maxX;
    [Tooltip("Batas minimal dan maksimal koordinat Y di map")]
    public float minY;
    public float maxY;

    void Start()
    {
        timer = spawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnBuffRandomly();
            timer = spawnInterval;
        }
    }

    void SpawnBuffRandomly()
    {
        if (buffPrefabs.Length == 0) return;

        // 1. Pilih Prefab secara acak (Heal atau Shield)
        int randomBuffIndex = Random.Range(0, buffPrefabs.Length);
        GameObject selectedBuff = buffPrefabs[randomBuffIndex];

        // 2. Tentukan posisi koordinat X dan Y secara ril acak sesuai batas map
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);

        // 3. Spawn Buff di posisi tersebut
        Instantiate(selectedBuff, spawnPosition, Quaternion.identity);
    }

    // Menampilkan kotak panduan area spawn warna kuning di Scene View Editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 center = new Vector3((minX + maxX) / 2, (minY + maxY) / 2, 0f);
        Vector3 size = new Vector3(maxX - minX, maxY - minY, 1f);
        Gizmos.DrawWireCube(center, size);
    }
}