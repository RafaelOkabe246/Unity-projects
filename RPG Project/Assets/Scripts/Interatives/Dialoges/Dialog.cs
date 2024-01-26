using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{

    public TextMeshProUGUI textDisplay;
    
    public string[] sentences;

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
        if(textDisplay.text == sentences[index])
        {
            continueButton.SetActive(true);
        }


        
    }

    public void Start_Setences()
    {
        
         StartCoroutine(Type());


    }

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void nextSentence()
    {
        continueButton.SetActive(false);

        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            index = 0;
            textDisplay.text = "";
            DialogueManager.Is_talking = false;
            continueButton.SetActive(false);
            Girl.canMove = true;
        }

        //Programar a nest sentence
    }
}
