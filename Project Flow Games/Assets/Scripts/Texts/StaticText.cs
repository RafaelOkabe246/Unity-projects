using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StaticText : WordDisplay
{
    public WordManager wordManager;
    public UnityEvent textEvents;
    public GameObject[] BigTexts;
    private bool selected;

    private void Start()
    {
        Word wordSelf = new Word(text.text, this);
        wordManager.words.Add(wordSelf);
        text.enabled = false;
    }

    public override void RemoveLetter()
    {
        if (!selected) {
            selected = true;
            foreach (GameObject gm in BigTexts) {
                gm.SetActive(false);
            }
            text.enabled = true;
        }
        base.RemoveLetter();
    }

    public override void RemoveWord()
    {
        //base.RemoveWord();
        //Destroy the gameObject 
        //Play the "button typing event"
        textEvents.Invoke();
    }
}
