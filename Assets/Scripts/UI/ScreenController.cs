using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private float _sceneFadeDuration;

    private SceneFadeTransition _sceneFade;

    private void Awake()
    {
        _sceneFade = GetComponentInChildren<SceneFadeTransition>();
    }

    private IEnumerator Start()
    {
        yield return _sceneFade.FadeInCoroutine(_sceneFadeDuration);
    }

    public void LoadScene(int sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    public void NextScene()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        LoadScene(nextScene);
    }

    public void RestartScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        LoadScene(currentScene);
    }

    private IEnumerator LoadSceneCoroutine(int sceneName)
    {
        yield return _sceneFade.FadeOutCoroutine(_sceneFadeDuration);
        yield return SceneManager.LoadSceneAsync(sceneName);
    }
}