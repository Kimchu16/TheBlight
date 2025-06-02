using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Enemies")]
    public List<GameObject> enemyPrefabs;
    public Transform[] enemySpawnPoints;
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    [Header("Game Settings")]
    public GameStateSO gameState;

    private bool isGameOver = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        if (enemyPrefabs.Count == 0 || enemySpawnPoints.Length == 0)
        {
            Debug.LogError("Enemy Prefabs or Spawn Points not assigned!");
            return;
        }

        foreach (Transform spawnPoint in enemySpawnPoints)
        {
            //int randomIndex = Random.Range(0, enemyPrefabs.Count);
            GameObject enemy = Instantiate(enemyPrefabs[0], spawnPoint.position, Quaternion.identity);
            spawnedEnemies.Add(enemy);
        }
    }

    public void OnPlayerDeath()
    {
        if (isGameOver) return;
        isGameOver = true;
        Debug.Log("Player died! Game Over!");
        GameOver(false); // Lose
    }

    public void OnAllEnemiesDefeated()
    {
        if (isGameOver) return;
        isGameOver = true;
        Debug.Log("All enemies defeated! You win!");
        GameOver(true); // Win
    }

    private void GameOver(bool won)
    {
        if (won)
        {
            // Save Progress to Next Scene
            gameState.sceneProgress.lastScene += 1;
            gameState.SaveAll();
        }

        // TODO: Fade to Game Over or Win Screen
       
    }

    public void CheckEnemiesRemaining()
    {
        // Remove dead enemies
        spawnedEnemies.RemoveAll(enemy => enemy == null);

        if (spawnedEnemies.Count == 0)
        {
            OnAllEnemiesDefeated();
        }
    }
}
