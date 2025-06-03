using UnityEngine;

public class SceneUIManager : MonoBehaviour
{
    public GameObject victoryPanel;
    public GameObject gameOverPanel;

    void Start()
    {
        // Register with GameManager
        GameManager.Instance.RegisterUI(this);
    }
}
