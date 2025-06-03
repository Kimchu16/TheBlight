using UnityEngine;
using Audio;

public class EscapeMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject escapeMenu;  // Reference to the Menu panel
    private SceneController _sceneController;

    private bool isPaused = false;

     private void Awake()
    {
        _sceneController = SceneController.Instance;
    }
    
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
        AudioManager.Instance.PlaySFX(SFXType.MenuClick);
        
    }

    public void ResumeGame()
    {
        escapeMenu.SetActive(false);
        Time.timeScale = 1f; // Resume the game
        isPaused = false;
        AudioManager.Instance.PlaySFX(SFXType.MenuClick);

    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f; // Resume time before switching scenes
        // Load your main menu scene here
        _sceneController.LoadScene(2);
    }
}
