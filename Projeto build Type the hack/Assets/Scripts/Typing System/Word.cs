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
            case char a when (a == 'ã' || a == 'Ã' || 
            a == 'â' || a == 'Â' || 
            a == 'á' || a == 'Á' || 
            a == 'à' || a == 'À'):
                return 'a';
            case char e when (e == 'é' || e == 'É' ||
            e == 'ê' || e == 'Ê' ||
            e == 'è' || e == 'È'):
                return 'e';
            case char i when (i == 'í' || i == 'Í' ||
            i == 'î' || i == 'Î' ||
            i == 'ì' || i == 'Ì'):
                return 'i';
            case char o when (o == 'ó' || o == 'Ó' ||
            o == 'ô' || o == 'Ô' ||
            o == 'ò' || o == 'Ò'):
                return 'o';
            case char u when (u == 'ú' || u == 'Ú' ||
            u == 'û' || u == 'Û' ||
            u == 'ù' || u == 'Ù'):
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

