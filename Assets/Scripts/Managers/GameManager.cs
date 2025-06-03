using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject victoryPanel;
    public GameObject gameOverPanel;

    [Header("Managers")]
    public EnemyManagerV2 enemyManager;

    [Header("Level Settings")]
    public int startingEnemies = 20;
    public float startingSpawnInterval = 3f;
    private int currentLevel = 1;

    void Awake()
    {
        if (Instance == null && Instance != this)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);  
            SceneManager.sceneLoaded += OnSceneLoaded;  // Subscribe to scene loaded event
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartLevel(currentLevel);
    }

    private void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scene Loaded: {scene.name}");

        // Find panels by name
        victoryPanel = GameObject.Find("VictoryPanel");
        gameOverPanel = GameObject.Find("GameOverPanel");

        if (victoryPanel == null) Debug.LogWarning("VictoryPanel not found in scene!");
        if (gameOverPanel == null) Debug.LogWarning("GameOverPanel not found in scene!");
    }

    public void StartLevel(int level)
    {
        Debug.Log($"Starting Level {level}");

        // Set enemy manager parameters based on level
        int enemiesToSpawn = startingEnemies + (level * 5); // Increase enemies each level
        float spawnInterval = Mathf.Max(1f, startingSpawnInterval - (level * 0.2f)); // Decrease spawn time each level (harder)

        enemyManager.SetMaxEnemies(enemiesToSpawn);
        enemyManager.SetSpawnInterval(spawnInterval);
        enemyManager.ResetSpawner();
    }

    public void NextLevel()
    {
        currentLevel++;
        StartLevel(currentLevel);
    }

    // Example win/lose condition
    public void GameOver()
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        
    }

    public void Victory()
    {
        Debug.Log("You Win!");
        Time.timeScale = 0f;
        victoryPanel.SetActive(true); 
    }
}
