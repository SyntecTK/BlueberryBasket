using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [Header("Spawn Settings")]
    public GameObject enemyPrefab;         // Prefab to spawn
    public Transform spawnPoint;           // Where to spawn it
    public float spawnInterval = 5f;       // Time between spawns (in seconds)

    private float timer;

    void Update() {
        timer += Time.deltaTime;

        if (timer >= spawnInterval) {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy() {
        if (enemyPrefab != null && spawnPoint != null) {
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        } else {
            Debug.LogWarning("Spawner missing enemyPrefab or spawnPoint.");
        }
    }
}
