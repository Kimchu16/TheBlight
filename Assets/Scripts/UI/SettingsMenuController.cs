using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SettingsMenuController : MonoBehaviour
{
    public GameObject audioSettingsPanel;
    public GameObject displaySettingsPanel;
    public GameObject otherSettingsPanel;
    public GameObject audioBtn;

    public GameStateSO gameState;
    public SceneProgressSO sceneProgress;
    // public PlayerSaveSO playerSave; // Uncomment when PlayerSaveSO is implemented
    private SceneController _sceneController;
    public Button continueButton;
    public Button newGameButton;

    [Header("Debug/Test Only")]
    public bool eraseSceneProgressInInspector;
    public bool erasePlayerSaveInInspector;

    void Start()
    {
        ShowAudioSettings(); // Default tab on open
    }

    void Awake()
    {
        _sceneController = SceneController.Instance;    
    }

    public void ShowAudioSettings()
    {
        EventSystem.current.SetSelectedGameObject(audioBtn);
        audioSettingsPanel.SetActive(true);
        displaySettingsPanel.SetActive(false);
        otherSettingsPanel.SetActive(false);
    }

    public void ShowDisplaySettings()
    {
        audioSettingsPanel.SetActive(false);
        displaySettingsPanel.SetActive(true);
        otherSettingsPanel.SetActive(false);
    }

    public void ShowOtherSettings()
    {
        audioSettingsPanel.SetActive(false);
        displaySettingsPanel.SetActive(false);
        otherSettingsPanel.SetActive(true);
    }

    // Call this from the Inspector for testing
    [ContextMenu("Reset Selected Saves (Inspector Only)")]
    public void ResetSelectedSavesInspector()
    {
        if (eraseSceneProgressInInspector)
        {
            string path = Application.persistentDataPath + "/scene_progress.json";
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);
            sceneProgress.Load();
            Debug.Log("Scene progress reset.");
        }

        if (erasePlayerSaveInInspector /* && playerSave != null */)
        {
            // string path = playerSave.GetPath();
            // if (System.IO.File.Exists(path))
            //     System.IO.File.Delete(path);
            // playerSave.Load();
            Debug.Log("Player save reset.");
        }

        if (continueButton != null && newGameButton != null)
        {
            continueButton.gameObject.SetActive(false);
            newGameButton.gameObject.SetActive(true);
            Debug.Log("UI buttons reset.");
        }
    }
}


