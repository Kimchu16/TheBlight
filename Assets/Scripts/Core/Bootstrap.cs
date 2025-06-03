using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject); // Makes PersistentManagers persistent
    }

    private void Start()
    {
        // Load your TitleScreen after managers are ready
        if (SceneManager.GetActiveScene().name == "PersistentManagers")
        {
            SceneManager.LoadScene(1);
        }
    }
}
