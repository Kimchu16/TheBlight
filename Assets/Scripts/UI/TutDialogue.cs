using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEditor.SearchService;
// using UnityEngine.SceneManagement; //remove

public class TutDialogue : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI dialogueText;
    public Button continueButton;

    [TextArea(3, 10)]
    public string[] dialogueLines;
    public float typingSpeed = 0.01f;
    private int index;
    public int CurrentIndex => index;
    private SceneController _SceneController;

    //intro
    [SerializeField] private GameObject startScreenPrefab;

    void Awake()
    {
        _SceneController = SceneController.Instance;
    }

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

        if (index == 3)
        {
            yield return new WaitForSeconds(1f); // Wait for 1 second before showing the continue button
            GameManager.Instance.TutorialEnemySpawn(); // Spawn enemies after the dialogue
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

            startScreenPrefab = Instantiate(startScreenPrefab);

            IntroSequence sequence = startScreenPrefab.GetComponent<IntroSequence>();
            if (sequence != null)
            {
                //_SceneController.LoadScene(4);
                sequence.shouldLoadScene = true;
                sequence.sceneToLoad = 4;
            }
             // Load next scene or perform any other action
        }
    }
    
    public void ResumeDialogueAfterCombat()
    {
        dialogueText.text = "";
        StartCoroutine(TypeLine());
    }

}
