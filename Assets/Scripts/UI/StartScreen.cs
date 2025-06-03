using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    [SerializeField]
    private SceneController _sceneController;

    private void Awake()
    {
        _sceneController = SceneController.Instance;
    }
    public void StartGame()
    {
        _sceneController.LoadScene(2);
    }
}
