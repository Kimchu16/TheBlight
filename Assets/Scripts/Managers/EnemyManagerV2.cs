using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EnemyType
{
    Basic,
    Rare
}

[System.Serializable]
public class EnemyData
{
    public GameObject prefab;
    public EnemyType type;
    [Range(0f, 1f)] public float spawnProbability;
}

public class EnemyManagerV2 : MonoBehaviour
{
    [Header("Enemy Settings")]
    public List<EnemyData> enemyDataList;
    public GameObject bossPrefab;
    public Transform[] spawnPoints;

    [Header("Spawn Settings")]
    public float spawnInterval = 3f;
    public int maxEnemies = 20;
    public int bossThreshold = 15;

    [Header("Boss Settings")]
    public float bossSpawnDelay = 3f;   // Delay before boss appears
    public bool pauseSpawningDuringBossEntrance = true;

    private float timer = 0f;
    private int totalSpawned = 0;
    private bool bossSpawned = false;
    private bool isPausedForBoss = false;
    public bool allowSpawning = true; // Karl added this 

    void Update()
    {
        if (isPausedForBoss || !allowSpawning) return;  // Don't spawn while waiting for boss entrance (Karl added something here too)

        timer += Time.deltaTime;

        if (timer >= spawnInterval && totalSpawned < maxEnemies)
        {
            if (!bossSpawned && totalSpawned >= bossThreshold)
            {
                StartCoroutine(SpawnBossWithDelay());
                bossSpawned = true;
            }
            else
            {
                SpawnEnemy();
            }

            timer = 0f;
        }
    }

    public void SpawnEnemy() // karl changed to public
    {
        if (spawnPoints.Length == 0 || enemyDataList.Count == 0) return;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Vector3 spawnPos = GetRandomizedPosition(spawnPoint.position);

        GameObject selectedEnemy = SelectEnemyBasedOnProbability();
        Instantiate(selectedEnemy, spawnPos, Quaternion.identity);

        totalSpawned++;
    }

    IEnumerator SpawnBossWithDelay()
    {
        if (pauseSpawningDuringBossEntrance)
        {
            isPausedForBoss = true; // Pause normal spawning
        }

        // (Optional) Play warning sound, flash screen, etc.
        Debug.Log("Boss incoming...");

        // Wait for dramatic pause
        yield return new WaitForSeconds(bossSpawnDelay);

        // Now spawn the boss
        if (spawnPoints.Length == 0 || bossPrefab == null) yield break;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Vector3 spawnPos = GetRandomizedPosition(spawnPoint.position);

        Instantiate(bossPrefab, spawnPos, Quaternion.identity);
        Debug.Log("Boss Spawned!");

        isPausedForBoss = false; // Resume normal spawning (if any)
    }

    Vector3 GetRandomizedPosition(Vector3 basePos)
    {
        float offsetX = Random.Range(-2f, 2f);
        float offsetY = Random.Range(-2f, 2f);
        return new Vector3(basePos.x + offsetX, basePos.y + offsetY, -1f);
    }

    GameObject SelectEnemyBasedOnProbability()
    {
        float totalProbability = 0f;
        foreach (var enemy in enemyDataList)
        {
            totalProbability += enemy.spawnProbability;
        }

        float randomPoint = Random.value * totalProbability;

        foreach (var enemy in enemyDataList)
        {
            if (randomPoint < enemy.spawnProbability)
            {
                return enemy.prefab;
            }
            else
            {
                randomPoint -= enemy.spawnProbability;
            }
        }

        // Fallback
        return enemyDataList[0].prefab;
    }

    public void ResetSpawner()
    {
        totalSpawned = 0;
        bossSpawned = false;
        isPausedForBoss = false;
        timer = 0f;
    }

    public void SetSpawnInterval(float newInterval)
    {
        spawnInterval = newInterval;
    }

    public void SetMaxEnemies(int newMax)
    {
        maxEnemies = newMax;
    }
}
