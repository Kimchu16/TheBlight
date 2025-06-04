using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    private SceneController _sceneController;
    public GameStateSO gameState;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject); // Makes PersistentManagers persistent
        _sceneController = SceneController.Instance;
    }

    private void Start()
    {
        // Load your TitleScreen after managers are ready
        if (_sceneController.GetActiveScene() == 0)
        {
            Debug.Log("Loading Title Screen from Bootstrap");
            _sceneController.LoadScene(1); // Load Title Screen
        }
        
        gameState.LoadAll(); // Load game state data
        
    }
}
