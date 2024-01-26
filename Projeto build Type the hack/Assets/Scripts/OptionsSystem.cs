using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public enum WordsMode
{
    Custom,
    Default
}

public class OptionsSystem : MonoBehaviour
{
    //Responsible to configurate the game settings


    [Header("Word customization")]
    public int maxWords;
    public List<string> showCustomWordsList;
    public static List<string> customWordsList = new List<string>();

    public TMP_InputField inputFieldAddWord;

    public GameObject wordListOrganizer;


    public TMP_Text wordListPrefab;
    public TextMesh showWords;

    public TMP_Text boxWordText;
    public string word;

    //public TMP_Text showWords;
    //public TMP_Text customWordsMode;
    
    public WordsMode _WordsMode;
    //public GameObject defaultButton, customButton;

    public StaticText wordInList;

    #region Word_custom
    private void Start()
    {
        ShowWordList();

        if (_WordsMode == WordsMode.Custom)
        {
            SetCustom();
        }
        else if (_WordsMode == WordsMode.Default)
        {
            SetDefault();
        }
    }


    public void ShowWordList()
    {
        showCustomWordsList = customWordsList;

        showWords.text = "";
        foreach (string word in showCustomWordsList)
        {
            //TMP_Text newWord = Instantiate(wordListPrefab, wordListOrganizer.transform.position, Quaternion.identity);
            //newWord.text = word;
            showWords.text += word + ", ";
        }
    }

    public void ChangeCustom()
    {
        //defaultButton.SetActive(true);
        //customButton.SetActive(true);
    }

    public void SetWordsMode(int c)
    {
        switch (c)
        {
            case (0):
                _WordsMode = WordsMode.Default;
                //Default mode selected
                SetDefault();
                break;
            case (1):
                _WordsMode = WordsMode.Custom;
                //Custom mode selected
                SetCustom();
                break;
        }
    }

    void SetCustom()
    {
        WordGenerator.isCustom = true;
        WordGenerator.wordsCustom = customWordsList;
    }

    void SetDefault()
    {
        WordGenerator.isCustom = false;
    }


    public void AddingWords()
    {
        Debug.Log("Add news words");
        inputFieldAddWord.text += " ";
        isAddingWord = false;

        //List<string> wordsTyped = new List<string>();
        string inputText = inputFieldAddWord.text;

        string currentWord = "";
        int wordIndex = 0;

        for (int i = 0; i < inputText.Length; i++)
        {
            //Não tem palavra
            if (inputText[i].ToString() == " " && currentWord != "")
            {
                
                bool hasUpperCase = currentWord.Any(x => char.IsUpper(x));
                if (hasUpperCase)
                {
                    currentWord = currentWord.ToLower();
                }

                Debug.Log("TESTE");
                customWordsList.Add(currentWord);
                wordIndex += 1;
                currentWord = "";
            }
            //Tem palavra
            else if (inputText[i].ToString() != " ")
            {
                currentWord += inputText[i];
            }
        }

        ShowWordList();
        word = "";
        inputFieldAddWord.text = "";
        WordInput.canType = true;
    }

    bool isAddingWord;
    public void StartWrittingWord()
    {
        boxWordText.text = "";
        isAddingWord = true;
        WordInput.canType = false;
        
    }

    void WrittingWord()
    {
        if (isAddingWord)
        {
            foreach (char letter in Input.inputString)
            {
                if (Input.anyKey)
                {
                    boxWordText.text += letter;
                }
            }
            
            word = boxWordText.text;
        }
    }

    private void Update()
    {
        WrittingWord();
    }



    #endregion

}
