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
    private Dictionary<SFXType, AudioSource> continuousSFXSources = new Dictionary<SFXType, AudioSource>();

    [Header("BGM Clips")]
    [SerializeField] private List<BGMClip> bgmClipsList;

    private Dictionary<BGMType, AudioClip> bgmClipsDict;


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

        //Setup dictionary for bgm
        bgmClipsDict = new Dictionary<BGMType, AudioClip>();
        foreach (var bgmClip in bgmClipsList)
        {
            if (!bgmClipsDict.ContainsKey(bgmClip.type))
            {
                bgmClipsDict.Add(bgmClip.type, bgmClip.clip);
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

    public void PlayContinuousSFX(SFXType type)
    {
        if (sfxClipsDict.TryGetValue(type, out AudioClip clip))
        {
            if (!continuousSFXSources.ContainsKey(type))
            {
                AudioSource newSource = gameObject.AddComponent<AudioSource>();
                newSource.loop = true;
                newSource.clip = clip;
                newSource.Play();
                continuousSFXSources[type] = newSource;
            }
            else if (!continuousSFXSources[type].isPlaying)
            {
                continuousSFXSources[type].Play();
            }
        }
        else
        {
            Debug.LogWarning($"Continuous SFX {type} not found!");
        }
    }
    
    public void StopContinuousSFX(SFXType type)
    {
        if (continuousSFXSources.TryGetValue(type, out AudioSource source))
        {
            source.Stop();
            Destroy(source); // clean up to prevent AudioSource spam
            continuousSFXSources.Remove(type);
        }
    }

    public void PlayBGM(BGMType type)
    {
        if (bgmClipsDict.TryGetValue(type, out AudioClip clip))
        {
            //if (bgmSource.clip == clip) return; // Don't restart same clip
            bgmSource.clip = clip;
            bgmSource.loop = true;
            Debug.Log("Playing BGM: " + clip);
            bgmSource.Play();
        }
        else
        {
            Debug.LogWarning($"BGM {type} not found!");
        }
    }

    public void StopBGM()
    {
        bgmSource.Stop();
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

[System.Serializable]
public class BGMClip
{
    public BGMType type;
    public AudioClip clip;
}
