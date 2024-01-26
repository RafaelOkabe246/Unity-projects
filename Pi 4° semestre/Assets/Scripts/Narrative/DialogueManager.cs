using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    TriggerDialogueObject currentDialogueObj;
    [SerializeField] NarrationLine currentDialogue;

    [SerializeField] private int sentencesIndex;
    public NarrationText[] dialogueSentences;

    [SerializeField] private int linesIndex;
    public string[] textLines;

    [Header("Screen")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public float typpingSpeed;
    private bool isTypping;

    private void Awake()
    {
        instance = this;
    }

    public void TriggerDialogue(Component component, object data)
    {
        sentencesIndex = 0;
        linesIndex = 0;

        if (component is TriggerDialogueObject)
        {
            currentDialogueObj = (TriggerDialogueObject)component;
        }
        currentDialogue = currentDialogueObj.narrationLine;

        dialogueSentences = currentDialogue.sentences;

        textLines = dialogueSentences[sentencesIndex].texts;


        DisplayCurrentLine();
    }

    IEnumerator TypeTextLine(string currentSentence)
    {
        typpingSpeed = 0.1f;
        isTypping = true;


        dialogueText.SetText("");
        foreach (char _char in currentSentence.ToCharArray())
        {
            dialogueText.text += _char;
            yield return new WaitForSeconds(typpingSpeed);
        }

        isTypping = false;

    }

    public void DisplayCurrentLine()
    {

        StopCoroutine(nameof (TypeTextLine));

        dialogueText.text = "";
        nameText.text = dialogueSentences[sentencesIndex].name;

        StartCoroutine(TypeTextLine(textLines[linesIndex]));

    }

    public void DisplayNextLine()
    {
        if (isTypping)
        {
            //Finish current line fast
            typpingSpeed = 0.01f;
            return;
        }


        if (linesIndex < textLines.Length - 1)
        {
            linesIndex++;
        }
        else if (linesIndex == textLines.Length - 1)
        {
            //Display next sentence
            Debug.Log("Mostre a próxima sentença");
            DisplayNextSentence();
            return;
        }

        DisplayCurrentLine();
    }

    public void DisplayNextSentence()
    {
        linesIndex = 0;

        if(sentencesIndex < dialogueSentences.Length -1)
        {
            sentencesIndex++;
        }
        else if(sentencesIndex == dialogueSentences.Length - 1)
        {
            //End dialogue
            Debug.Log("Dialogue end");
            CloseDialogue();
            return;
        }

        textLines = dialogueSentences[sentencesIndex].texts;

        DisplayCurrentLine();

    }

    public void CloseDialogue()
    {
        ScreenStack.instance.RemoveScreenFromStack(GameplayUIsContainer.instance.GetDialogueScreen());
    }
}
