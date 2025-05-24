using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    [SerializeField]
    private SceneController _sceneController;
    public void StartGame()
    {
        _sceneController.LoadScene("MainMenu");
    }
}
