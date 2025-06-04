using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public void Start()
    {
        continueButton.onClick.AddListener(NextLine);
        StartDialogue();
    }

    public void StartDialogue()
    {
        index = 0;
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
            if (index == dialogueLines.Length - 1)
            {
               continueButton.GetComponentInChildren<TextMeshProUGUI>().text = "Ready";
            }
            StartCoroutine(TypeLine());
        }
        else
        {
            // End of Dialogue
            Debug.Log("Dialogue finished.");
            SceneManager.LoadScene(4); // Load next scene or perform any other action
        }
    }

}
