using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* The word itself. It contains all the methods and informations a word should have
*/
[System.Serializable]
public class Word 
{

    public string word; //the word itself
    private int typeIndex;

    public WordDisplay display;

    public Word(string _word, WordDisplay _display)
    {
        word = _word;
        typeIndex = 0;

        display = _display;
        display.SetWord(word);
    }

    public void ReWord(string _word) {
        word = _word;
        typeIndex = 0;

        display.SetWord(word);
    }

    public char GetNextLetter()
    {
        if (typeIndex >= word.Length) {
            typeIndex-= 1;
        }
        switch (word[typeIndex]) 
        {
            case char a when (a == '�' || a == '�' || 
            a == '�' || a == '�' || 
            a == '�' || a == '�' || 
            a == '�' || a == '�'):
                return 'a';
            case char e when (e == '�' || e == '�' ||
            e == '�' || e == '�' ||
            e == '�' || e == '�'):
                return 'e';
            case char i when (i == '�' || i == '�' ||
            i == '�' || i == '�' ||
            i == '�' || i == '�'):
                return 'i';
            case char o when (o == '�' || o == '�' ||
            o == '�' || o == '�' ||
            o == '�' || o == '�'):
                return 'o';
            case char u when (u == '�' || u == '�' ||
            u == '�' || u == '�' ||
            u == '�' || u == '�'):
                return 'u';
            default:
                return word[typeIndex];
        }

    }

    public void TypeLetter()
    {
        // Remove the letter on screen
        if(typeIndex < word.Length)
            typeIndex++;
        display.RemoveLetter();
        
    }

    public bool WordTyped()
    {
        bool wordTyped = (typeIndex >= word.Length);
        if (wordTyped)
        {
            // Remove the word on screen
            display.RemoveWord();
            display.ChangeColor(Color.white);
        }
        return wordTyped;
    }


}

