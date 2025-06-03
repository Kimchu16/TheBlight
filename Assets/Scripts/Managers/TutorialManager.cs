using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public TutDialogue tutDialogue;         // Reference to your dialogue manager
    public EnemyManagerV2 enemyManager;     // Reference to your EnemyManagerV2
    public GameObject dialoguePanel;        // The dialogue panel to show/hide

    private int enemiesToSpawn = 5;
    private int enemiesAlive = 0;

    void Start()
    {
        tutDialogue.continueButton.onClick.AddListener(OnDialogueNext);
        enemyManager.SetMaxEnemies(enemiesToSpawn);
        // enemyManager.SetSpawnInterval(spawnInterval);
        enemyManager.ResetSpawner();
    }

    void OnDialogueNext()
    {
        // If player clicked from element 4 (index 1), spawn enemies
        if (tutDialogue.CurrentIndex == 4)  // 2 is when they just pressed next on Element 2 (index starts at 0)
        {
            dialoguePanel.SetActive(false);  // Hide dialogue
            StartEnemyWave();
        }
    }

    void StartEnemyWave()
    {
        enemyManager.allowSpawning = false; //Make sure auto spawn stays OFF
        enemiesAlive = enemiesToSpawn;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            enemyManager.SpawnEnemy();
        }
    }

    // Call this from enemy death script
    public void OnEnemyKilled()
    {
        enemiesAlive--;

        Debug.Log("Enemy killed. Enemies alive: " + enemiesAlive);

        if (enemiesAlive <= 0)
        {
            // All enemies are dead, continue tutorial
            Debug.Log("All enemies killed. Continuing tutorial.");
            dialoguePanel.SetActive(true);
            tutDialogue.NextLine(); // Move to next line (continue tutorial)
            // FindFirstObjectByType<TutorialManager>().OnEnemyKilled();
        }
    }
}

