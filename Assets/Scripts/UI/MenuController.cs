using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    private SceneController _sceneController;  

    [Header("References")]
    public GameStateSO gameState;
    public Button continueButton;
    public Button newGameButton;

    private void Awake()
    {
        _sceneController = SceneController.Instance;
    }

    void Start()
    {
        if (gameState == null)
        {
            Debug.LogError("GameState is missing!");
            return;
        }

        // Only run this logic if the buttons exist
        if (continueButton != null && newGameButton != null)
        {
            // Disable Continue button if no saved scene exists
            if (gameState.sceneProgress.lastScene < 3) // Saved scene is title or menu, no saved stage
            {
                continueButton.gameObject.SetActive(false);
            }
            else if (gameState.sceneProgress.lastScene >= 3)
            {
                newGameButton.gameObject.SetActive(false);
            }
        }
    }

    public void NewGame(){
        gameState.sceneProgress.lastScene = 3;  // Set starting scene
        gameState.SaveAll();                
        _sceneController.LoadScene(3);
    }

    public void ContinueGame(){
        gameState.LoadAll(); // Load saved data
        _sceneController.LoadScene(gameState.sceneProgress.lastScene);  // Resume from last scene saved
    }

    public void RestartGame()
    {
        _sceneController.RestartScene();
    }

    public void NextScene()
    {
        _sceneController.NextScene();
    }

    public void MainMenu()
    {
        gameState.SaveAll();
        _sceneController.LoadScene(2);
    }

    public void QuitGame(){
        Application.Quit();
    }
}