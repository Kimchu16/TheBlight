using UnityEngine;

public class EscapeMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject escapeMenu;  // Reference to the Menu panel
    public SceneController sceneController;
    [SerializeField]private MenuButtonSFX menuButtonSFX;
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        escapeMenu.SetActive(true);
        Time.timeScale = 0f; // Pause the game
        isPaused = true;

        if (menuButtonSFX != null){
            menuButtonSFX.PlaySound(); 
        }
    }

    public void ResumeGame()
    {
        escapeMenu.SetActive(false);
        Time.timeScale = 1f; // Resume the game
        isPaused = false;

        if (menuButtonSFX != null){
            menuButtonSFX.PlaySound(); 
        }
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f; // Resume time before switching scenes
        // Load your main menu scene here
        sceneController.LoadScene(1);
    }
}
