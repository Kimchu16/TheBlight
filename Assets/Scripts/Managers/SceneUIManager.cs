using UnityEngine;
using UnityEngine.SceneManagement;
using Audio;

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

        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Im on scene:"+sceneIndex);

        switch (sceneIndex)
        {
            case 4: //lvl 1
                AudioManager.Instance.PlayBGM(BGMType.Level1);
                break;
            case 5: //lvl2
                AudioManager.Instance.PlayBGM(BGMType.Level2);
                break;
            case 6: //lvl3
                AudioManager.Instance.PlayBGM(BGMType.Level3);
                break;
            default:
                AudioManager.Instance.PlayBGM(BGMType.MainBGM);
                break;
        }
       
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
