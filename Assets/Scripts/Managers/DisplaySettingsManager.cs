using UnityEngine;
using UnityEngine.UI;

public class DisplaySettingsManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Selector resolutionSelector;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Toggle vsyncToggle;

    [Header("ApplyButton")]
    [SerializeField] private Animator applyConfirmAnimator;

    private void Start()
    {
        LoadDisplaySettings();
    }

    private void LoadDisplaySettings()
    {
        // Load Fullscreen
        bool isFullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        Screen.fullScreen = isFullscreen;
        fullscreenToggle.isOn = isFullscreen;

        // Load VSync
        int vsync = PlayerPrefs.GetInt("VSync", 1);
        QualitySettings.vSyncCount = vsync;
        vsyncToggle.isOn = vsync > 0;

        // Load Resolution
        int savedResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", 0);
        resolutionSelector.SetResolution(savedResolutionIndex);
    }

    public void ApplyDisplaySettings()
    {
        // Apply Fullscreen
        bool isFullscreen = fullscreenToggle.isOn;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        Screen.fullScreen = isFullscreen;

        // Apply VSync
        int vsync = vsyncToggle.isOn ? 1 : 0;
        PlayerPrefs.SetInt("VSync", vsync);
        QualitySettings.vSyncCount = vsync;

        // Apply Resolution
        int currentResolutionIndex = resolutionSelector.GetCurrentResolutionIndex();
        PlayerPrefs.SetInt("ResolutionIndex", currentResolutionIndex);

        Resolution selectedRes = resolutionSelector.GetSelectedResolution();
        Screen.SetResolution(selectedRes.width, selectedRes.height, isFullscreen);

        PlayerPrefs.Save();
    }

    public void PlayConfirmation()
    {
        applyConfirmAnimator.SetTrigger("PlayConfirm");
    }
}
