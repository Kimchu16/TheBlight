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

    void Start()
    {
        resolutions = Screen.resolutions;
        UpdateResolutionText();

        leftButton.onClick.AddListener(PreviousResolution);
        rightButton.onClick.AddListener(NextResolution);
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
}
