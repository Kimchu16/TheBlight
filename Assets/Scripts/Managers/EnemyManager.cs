using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;        // Prefab of the enemy to spawn
    public Transform[] spawnPoints;       // Array of possible spawn positions
    public float spawnInterval = 3f;      // Time in seconds between each spawn
    public int maxEnemies = 20;           // Maximum number of enemies to spawn
    private float timer = 0f;             // Timer to track time between spawns
    private int totalSpawned = 0;         // Tracks how many enemies have been spawned

    void Update()
    {
        timer += Time.deltaTime;          // Increment timer with time passed since last frame

        // Check if it's time to spawn a new enemy and the max has not been reached
        if (timer >= spawnInterval && totalSpawned < maxEnemies)
        {
            SpawnEnemy();                 // Call function to spawn an enemy
            timer = 0f;                   // Reset spawn timer
        }
    }

    // Spawns an enemy at a random spawn point with slight position offset
    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0) return; // Exit if no spawn points are set

        // Choose a random spawn point from the array
        int index = Random.Range(0, spawnPoints.Length);
        Vector3 basePos = spawnPoints[index].position;

        // Add small random offset to X and Y to vary spawn position
        float offsetX = Random.Range(-2f, 2f);
        float offsetY = Random.Range(-2f, 2f);

        // Final spawn position with random offset
        Vector3 spawnPos = new Vector3(basePos.x + offsetX, basePos.y + offsetY, -1f);

        // Instantiate the enemy prefab at the calculated position with no rotation
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        totalSpawned++; // Increase the count of total spawned enemies
    }
}
