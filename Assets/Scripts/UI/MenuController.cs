using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("References")]
    public GameStateSO gameState;
    public SceneController sceneController;     
    public Button continueButton;
    public Button newGameButton;

    void Start()
    {
        //Debug.Log("Loaded Scene Index before load: " + gameState.sceneProgress.lastScene);
        gameState.sceneProgress.Load(); // Load save file if any exists
        //Debug.Log("Loaded Scene Index after load: " + gameState.sceneProgress.lastScene);


        // Disable Continue button if no saved scene exists
        if (gameState.sceneProgress.lastScene < 3) // Saved scene is title or menu, aka no saved stage
        {
            continueButton.gameObject.SetActive(false);
        }
        else if (gameState.sceneProgress.lastScene > 3)
        {
            newGameButton.gameObject.SetActive(false);
        }
    }

    public void NewGame(){
        gameState.sceneProgress.lastScene = 2;  // Set starting scene
        gameState.SaveAll();                
        sceneController.LoadScene(2);
    }

    public void ContinueGame(){
        gameState.LoadAll(); // Load saved data
        sceneController.LoadScene(gameState.sceneProgress.lastScene);  // Resume from last scene saved
    }

    public void QuitGame(){
        Application.Quit();
    }
}