using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private SceneController _sceneController;
    public GameObject victoryPanel;
    public GameObject gameOverPanel;

    [Header("Managers")]
    public EnemyManagerV2 enemyManager;

    [Header("Level Settings")]
    public int startingEnemies = 5;
    public float startingSpawnInterval = 3f;
    public int currentLevel = 1;

    void Awake()
    {
        _sceneController = SceneController.Instance;
        if (Instance == null && Instance != this)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (_sceneController.GetActiveScene() > 3)
        {
            StartLevel(currentLevel);
        }
    }

    public void RegisterUI(SceneUIManager uiManager)
    {
        this.victoryPanel = uiManager.victoryPanel;
        this.gameOverPanel = uiManager.gameOverPanel;

        Debug.Log("UI registered to GameManager");
    }


    public void StartLevel(int level)
    {
        Debug.Log($"Starting Level {level}");

        // Set enemy manager parameters based on level
        int enemiesToSpawn = startingEnemies + (level * 5); // Increase enemies each level
        Debug.Log($"Enemies to spawn: {enemiesToSpawn}");
        float spawnInterval = Mathf.Max(1f, startingSpawnInterval - (level * 0.2f)); // Decrease spawn time each level (harder)

        if (enemyManager == null)
        {
            Debug.LogError("EnemyManager is not assigned in GameManager!");
            return;
        }
        enemyManager.SetMaxEnemies(enemiesToSpawn + 1); // +1 for the boss
        enemyManager.SetSpawnInterval(spawnInterval);
        enemyManager.SetBossThreshold(enemiesToSpawn);  // boss spawns after all regular enemies
        enemyManager.ResetSpawner();
    }

    public void NextLevel()
    {
        currentLevel++;
        Debug.Log($"Advancing to Level {currentLevel}");
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
