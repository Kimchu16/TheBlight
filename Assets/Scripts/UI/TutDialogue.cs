using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class TutDialogue : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI dialogueText;
    public Button continueButton;

    [TextArea(3, 10)]
    public string[] dialogueLines;
    public float typingSpeed = 0.05f;
    private int index;
    public int CurrentIndex => index;

    void Start()
    {
        continueButton.onClick.AddListener(NextLine);
        StartDialogue();
    }

    void StartDialogue()
    {
        index = 0;
        dialogueText.text = "";
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        dialogueText.text = "";
        foreach (char c in dialogueLines[index].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextLine()
    {
        if (index < dialogueLines.Length - 1)
        {
            index++;
            StartCoroutine(TypeLine());
        }
        else
        {
            // End of Dialogue
            gameObject.SetActive(false);
        }
    }

}
