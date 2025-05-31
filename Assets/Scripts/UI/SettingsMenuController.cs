using UnityEngine;
using UnityEngine.EventSystems;
public class SettingsMenuController : MonoBehaviour
{
    public GameObject audioSettingsPanel;
    public GameObject displaySettingsPanel;
    public GameObject otherSettingsPanel;
    public GameObject audioBtn;

    void Start()
    {
        ShowAudioSettings(); // Default tab on open
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
}


