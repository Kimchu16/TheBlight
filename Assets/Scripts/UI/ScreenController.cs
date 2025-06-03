using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; }

    [SerializeField]
    private float _sceneFadeDuration;

    private SceneFadeTransition _sceneFade;

    private void Awake()
    {
        _sceneFade = GetComponentInChildren<SceneFadeTransition>();

        if (Instance == null && Instance != this)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);  
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Start()
    {
        yield return _sceneFade.FadeInCoroutine(_sceneFadeDuration);
    }

    public void LoadScene(int sceneName)
    {
        Time.timeScale = 1f;
        Debug.Log("Starting scene load for scene: " + sceneName);
        StartCoroutine(LoadSceneCoroutine(sceneName));
        //SceneManager.LoadScene(sceneName);
    }

    public void NextScene()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        LoadScene(nextScene);
    }

    public void RestartScene()
    {
        Time.timeScale = 1f;
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        LoadScene(currentScene);
    }

    private IEnumerator LoadSceneCoroutine(int sceneName)
    {
       // 1. Fade Out
        yield return _sceneFade.FadeOutCoroutine(_sceneFadeDuration);

        // 2. Wait one frame to ensure the fade panel is on top
        yield return null;

        // 3. Start async scene load
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // 4. Wait until async loading is done
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // 5. Fade In
        yield return _sceneFade.FadeInCoroutine(_sceneFadeDuration);
    }
}