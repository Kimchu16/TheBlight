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

        //Debug.Log("Loaded Scene Index before load: " + gameState.sceneProgress.lastScene);
        gameState.sceneProgress.Load(); // Load save file if any exists
        //Debug.Log("Loaded Scene Index after load: " + gameState.sceneProgress.lastScene);


        // Disable Continue button if no saved scene exists
        if (gameState.sceneProgress.lastScene < 3) // Saved scene is title or menu, aka no saved stage
        {
            //Debug.Log("no save");
            continueButton.gameObject.SetActive(false);
        }
        else if (gameState.sceneProgress.lastScene >= 3)
        {
            //Debug.Log("save found");
            newGameButton.gameObject.SetActive(false);
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

    public void MainMenu()
    {
        gameState.SaveAll();
        _sceneController.LoadScene(1);
    }

    public void QuitGame(){
        Application.Quit();
    }
}