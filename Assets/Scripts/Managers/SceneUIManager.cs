using UnityEngine;

public class SceneUIManager : MonoBehaviour
{
    public GameObject victoryPanel;
    public GameObject gameOverPanel;
    public EnemyManagerV2 enemyManager;
    private SceneController _sceneController;  

    private void Awake()
    {
        _sceneController = SceneController.Instance;
    }

    void Start()
    {
        // Register with GameManager
        GameManager.Instance.RegisterUI(this);
        GameManager.Instance.enemyManager = enemyManager;
        Debug.Log($"[SceneUIManager] Assigned enemyManager: {enemyManager}");
       
        if (_sceneController.GetActiveScene() == 4) // If level 1 is being loaded
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
