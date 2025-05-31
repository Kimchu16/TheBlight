using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        LoadAudioSettings();

        // Setup listeners
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void LoadAudioSettings()
    {
        // Load saved values or default to 0.8f
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 0.8f);
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.8f);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.8f);

        masterSlider.value = masterVolume;
        musicSlider.value = musicVolume;
        sfxSlider.value = sfxVolume;

        // Apply loaded volumes
        AudioManager.Instance.SetMasterVolume(masterVolume);
        AudioManager.Instance.SetMusicVolume(musicVolume);
        AudioManager.Instance.SetSFXVolume(sfxVolume);
    }

    private void SetMasterVolume(float value)
    {
        AudioManager.Instance.SetMasterVolume(value);
        PlayerPrefs.SetFloat("MasterVolume", value); // Save immediately
    }

    private void SetMusicVolume(float value)
    {
        AudioManager.Instance.SetMusicVolume(value);
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    private void SetSFXVolume(float value)
    {
        AudioManager.Instance.SetSFXVolume(value);
        PlayerPrefs.SetFloat("SFXVolume", value);
    }

    private void OnDisable()
    {
        PlayerPrefs.Save(); // Make sure everything is saved
    }
}
