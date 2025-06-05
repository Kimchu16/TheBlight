using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [Header("Dialogue Settings")]       // The dialogue panel to show/hide
    public TutDialogue tutDialogue; // Reference to the tutorial dialogue script
    public GameObject dialoguePanel; // The panel that contains the dialogue UI

    [Header("Enemy Settings")]
    public EnemyManagerV2 enemyManager; // Reference to the enemy manager script

    [Header("Spawn Settings")]
    public int enemiesToSpawn = 3;
    public int enemiesAlive = 0;
    public bool waitingForEnemies = false; // Flag to check if we are waiting for

    void Start()
    {
        tutDialogue.continueButton.onClick.AddListener(OnDialogueNext);
        enemyManager.SetMaxEnemies(enemiesToSpawn);
        // enemyManager.SetSpawnInterval(spawnInterval);
        //enemyManager.ResetSpawner();
    }

    void OnDialogueNext()
    {
        // If player clicked from element 4 (index 1), spawn enemies
        Debug.Log("Dialogue Index: " + tutDialogue.CurrentIndex);
        
    }

    public void TriggerEnemyWave()
    {
        dialoguePanel.SetActive(false);  // Hide dialogue
        GameManager.Instance.TutorialEnemySpawn();
        enemiesAlive = enemiesToSpawn;
        waitingForEnemies = true;
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
            waitingForEnemies = false; // Reset flag for next wave
            tutDialogue.ResumeDialogueAfterCombat();
            // tutDialogue.NextLine(); // Move to next line (continue tutorial)
            // FindFirstObjectByType<TutorialManager>().OnEnemyKilled();
        }
    }
}

