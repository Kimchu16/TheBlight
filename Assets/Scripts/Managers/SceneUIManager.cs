using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneUIManager : MonoBehaviour
{
    public GameObject victoryPanel;
    public GameObject gameOverPanel;
    public EnemyManagerV2 enemyManager;

    void Start()
    {
        // Register with GameManager
        GameManager.Instance.RegisterUI(this);
        GameManager.Instance.enemyManager = enemyManager;
        Debug.Log($"[SceneUIManager] Assigned enemyManager: {enemyManager}");
       
        if (SceneManager.GetActiveScene().buildIndex == 4) // If level 1 is being loaded
        {
            GameManager.Instance.StartLevel(GameManager.Instance.currentLevel);
        }
        else // If 2nd level or more
        {
            GameManager.Instance.NextLevel();
            GameManager.Instance.StartLevel(GameManager.Instance.currentLevel);
        }
    }
    
}
