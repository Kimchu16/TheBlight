using UnityEngine;
using TMPro;
using System.Collections;

public class IntroSequence : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public TextMeshProUGUI textComponent;
    [TextArea] public string fullText;
    public float typingSpeed = 0.05f;

    public TextMeshProUGUI continueHint;
    public float fadeDuration = 1f;

    private bool typingDone = false;
    private bool waitingForInput = false;

    //load end scene
    public bool shouldLoadScene = false;
    public int sceneToLoad = 1;

    void Start()
    {
        canvasGroup.alpha = 0;
        continueHint.alpha = 0;
        StartCoroutine(PlayIntro());
    }

    IEnumerator PlayIntro()
    {
        // Fade in
        yield return FadeCanvas(0, 1, fadeDuration);

        // Typing effect
        yield return StartCoroutine(TypeText());

        // Show "Press any key"
        yield return new WaitForSeconds(1f);
        continueHint.alpha = 1;
        typingDone = true;
        waitingForInput = true;

        // Wait for input
        yield return new WaitUntil(() => Input.anyKeyDown);

        // Fade out
        yield return FadeCanvas(1, 0, fadeDuration);

        // Destroy or notify game
        Destroy(gameObject);
        // OR: GameManager.Instance.StartGame();
        if (shouldLoadScene)
        {
            SceneController.Instance.LoadScene(sceneToLoad);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator TypeText()
    {
        textComponent.text = "";
        for (int i = 0; i <= fullText.Length; i++)
        {
            textComponent.text = fullText.Substring(0, i);
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    IEnumerator FadeCanvas(float from, float to, float duration)
    {
        float elapsed = 0;
        while (elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = to;
    }

    void Update()
    {
        // Optional: also detect key press here if needed for other conditions
    }
}
