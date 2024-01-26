using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    public bool IsDialoguePlaying { get; private set; }

    private Story currentStory;

    [SerializeField] private TextMeshProUGUI dialogueText;

    //Choices
    [Header("Choices")]
    public GameObject MessageBox;
    
    [SerializeField] private GameObject[] choices;
    
    private TextMeshProUGUI[] choicesText;
    
    [SerializeField] 
    private GameObject continueButton;

    private void Start()
    {
        IsDialoguePlaying = false;

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    void Update()
    {
        if (!IsDialoguePlaying)
        {
            return;
        }
    }

    public void EnterDialogueMode(TextAsset InkJsonText)
    {
        foreach (GameObject gameObject in choices)
        {
            gameObject.SetActive(true);
        }
        MessageBox.SetActive(true);
        continueButton.SetActive(true);
        currentStory = new Story(InkJsonText.text);
        IsDialoguePlaying = true;
        ContinueStory();
    }

    private void ExitDialogueMode()
    {
        IsDialoguePlaying = false;
        dialogueText.text = "";
        MessageBox.SetActive(false);
    }

    public void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            // Display text
            dialogueText.text = currentStory.Continue();

            // Display choices if it has
            DisplayChoices();
        }
        else
        {
            //if ()
            //{
                ExitDialogueMode();
            //}
        }
    }

    private void DisplayChoices()
    {

        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("The UI cannot support more choices: " + currentChoices);
        }

        int index = 0;
        // enable and initialize the choices up to the amount of choices for this line of dialogue
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        // go throught the remanining choices the UI supports and make sure they are hidden
        for(int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
        if(index > 0)
        {
            continueButton.SetActive(false);
        }
        //
        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        
        currentStory.ChooseChoiceIndex(choiceIndex);

        //Hides the choices buttons
        foreach(GameObject gameObject in choices)
        {
            gameObject.SetActive(false);
        }

        //Activates the event 0 or 1 according to the index

        continueButton.SetActive(true);
    }

    

}
