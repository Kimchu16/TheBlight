using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Audio; 

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Volume Sliders")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip mainMenuBGM;
    [SerializeField] private AudioClip gameBGM;

    [Header("SFX Clips")]
    [SerializeField] private List<SFXClip> sfxClipsList;

    private Dictionary<SFXType, AudioClip> sfxClipsDict;

    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Setup Dictionary
        sfxClipsDict = new Dictionary<SFXType, AudioClip>();
        foreach (var sfxClip in sfxClipsList)
        {
            if (!sfxClipsDict.ContainsKey(sfxClip.type))
            {
                sfxClipsDict.Add(sfxClip.type, sfxClip.clip);
            }
        }
    }

    private void Start()
    {
        InitializeVolumeSettings();
    }
    private void InitializeVolumeSettings()
    {
        if (masterSlider != null)
        {
            SetMasterVolume(masterSlider.value);   // Set initial volume
            masterSlider.onValueChanged.AddListener(SetMasterVolume);  // Update when changed
        }

        if (musicSlider != null)
        {
            SetMusicVolume(musicSlider.value);
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
        }

        if (sfxSlider != null)
        {
            SetSFXVolume(sfxSlider.value);
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        }
    }

    public void PlaySFX(SFXType type)
    {
        if (sfxClipsDict.TryGetValue(type, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"SFX {type} not found!");
        }
    }

    public void PlayBGM(AudioClip clip)
    {
        if (bgmSource.clip == clip) return; // Don't replay same BGM
        bgmSource.clip = clip;
        bgmSource.Play();
    }

    public void SetMasterVolume(float volume)
    {
        float volumeDb = Mathf.Lerp(-60f, 0f, volume);
        audioMixer.SetFloat("MasterVolume", volumeDb);
    }

    public void SetMusicVolume(float volume)
    {
        float volumeDb = Mathf.Lerp(-40f, 0f, volume);
        audioMixer.SetFloat("MusicVolume", volumeDb);
    }

    public void SetSFXVolume(float volume)
    {
        float volumeDb = Mathf.Lerp(-40f, 0f, volume);
        audioMixer.SetFloat("SFXVolume", volumeDb);
    }
}

[System.Serializable]
public class SFXClip
{
    public SFXType type;
    public AudioClip clip;
}
