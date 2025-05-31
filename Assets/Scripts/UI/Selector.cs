using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Selector : MonoBehaviour
{
    public TextMeshProUGUI resolutionText;
    public Button leftButton;
    public Button rightButton;

    private Resolution[] resolutions;
    private int currentResolutionIndex = 0;

    void Awake()
    {
        resolutions = Screen.resolutions;
    }

    void Start()
    {
        UpdateResolutionText();

        leftButton.onClick.AddListener(PreviousResolution);
        rightButton.onClick.AddListener(NextResolution);
    }

    public void SetResolution(int index)
    {
        currentResolutionIndex = Mathf.Clamp(index, 0, resolutions.Length - 1);
        UpdateResolutionText();
    }

    public int GetCurrentResolutionIndex()
    {
        return currentResolutionIndex;
    }

    void PreviousResolution()
    {
        currentResolutionIndex = (currentResolutionIndex - 1 + resolutions.Length) % resolutions.Length;
        UpdateResolutionText();
    }

    void NextResolution()
    {
        currentResolutionIndex = (currentResolutionIndex + 1) % resolutions.Length;
        UpdateResolutionText();
    }

    void UpdateResolutionText()
    {
        var res = resolutions[currentResolutionIndex];
        resolutionText.text = res.width + "x" + res.height;
    }
    public Resolution GetSelectedResolution()
    {
        return resolutions[currentResolutionIndex];
    }
}
