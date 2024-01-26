using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBox : MonoBehaviour
{
    public Player player;
    public TMP_Text textDisplay;
    public GameObject dialoguePanel;
    //public string[] sentences;

    public DialogText sentences;

    [SerializeField]
    private int index;
    public float typingSpeed;

    public GameObject continueButton;

    void Start()
    {
        continueButton.SetActive(false);
        textDisplay.text = "";
    }

    void Update()
    {
        if(sentences != null)
          if (textDisplay.text == sentences.textLines[index])
            {
                continueButton.SetActive(true);
            }

    }

    public void AddTexts(DialogText dialogText)
    {
        sentences = dialogText;
    }

    public void Start_Setences()
    {
        dialoguePanel.SetActive(true);
        StartCoroutine(Type());
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences.textLines[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void nextSentence()
    {
        continueButton.SetActive(false);

        if (index < sentences.textLines.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            index = 0;
            textDisplay.text = "";
            continueButton.SetActive(false);
            dialoguePanel.SetActive(false);
            player._GameMode = GameMode.Shooting;
        }

        //Programar a nest sentence
    }
}
