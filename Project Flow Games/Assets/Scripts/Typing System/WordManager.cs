using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/*
* Create the words on scene and also check if the correct letter is being typed
*/
public class WordManager : MonoBehaviour
{
    public List<Word> words;
    public List<WordDisplay> enemiesDisplay;

    public WordSpawner wordSpawner;
    public WaveSystem waveSystem;

    public bool isWaveFinish;

    public bool hasActiveWord;
    public Word activeWord;
    public WordDisplay activeWordDisplay;

    public Player player;
    public bool isInMenu;
    public bool cantType;

    private void Start()
    {
        if(waveSystem == null)
        {
            isInMenu = true;
        }
        else
        {
            isInMenu = false;
        }
        foreach (var word in WordGenerator.wordsCustom)
        {
            Debug.Log(word);
        }

    }


    public void CheckWave() 
    {
        if (words.Count == 0 && isWaveFinish == false)
        {
            //Play next wave
            isWaveFinish = true;
            if (waveSystem != null)
            {
                Debug.Log("Call new wave");
                waveSystem.StartCoroutine(waveSystem.StartNewWave());
            }
        }
    }

    public void AddWord()
    {
        Word word = new Word(WordGenerator.GetRandomWord(), wordSpawner.SpawnWord());
        word.display.enemyWord = word;
        words.Add(word);
        enemiesDisplay.Add(word.display);
    }

    public void ReAddWord() {
        activeWord.ReWord(WordGenerator.GetRandomWord());
        hasActiveWord = false;
        int index = words.IndexOf(activeWord);
        Word newWord = activeWord;
        words.Insert(index, newWord);
        words.Remove(activeWord);
        activeWord = null;
    }

    public void TypeLetter(char letter)
    {
        if (!cantType)
        {
            string stringLetter = "" + letter;
            string letterLowerCase = stringLetter.ToLower();
            letter = letterLowerCase[0];

            

            if (hasActiveWord)
            {
                //Check if letter was next and remove it from the word
                if (activeWord.GetNextLetter() == letter)
                {
                    SoundSystem.instance.SearchSound(1, "");
                    activeWord.TypeLetter();
                    StartCoroutine(CameraController.CameraShake(2f, 2f, 0.1f));
                    if (player != null)
                    {
                        Player.CallPlayerTapAnimation();
                    }
                }
                else
                {
                    StartCoroutine(CameraController.CameraShake(2f, 2f, 0.1f));
                }
            }
            else
            {
                float nearestWordDistance = 100;
                Word nearestWord = null;
                foreach (Word word in words)
                {
                    //Make a list of words that have the same initial char

                    if (!isInMenu)
                    {
                        //Searching for the nearest letter from the player, only during playing scene
                        foreach (WordDisplay wordDisplay in enemiesDisplay)
                        {
                            if (wordDisplay.enemyWord.word.StartsWith(letter.ToString()) && wordDisplay.gameObject.activeSelf && Vector2.Distance(wordDisplay.transform.position, player.transform.position) < nearestWordDistance)
                            {
                                nearestWord = wordDisplay.enemyWord;
                                nearestWordDistance = Vector2.Distance(wordDisplay.transform.position, player.transform.position);
                            }
                        }
                        
                    }
                    



                    if (word.GetNextLetter() == letter && !isInMenu && word == nearestWord || word.GetNextLetter() == letter && isInMenu)
                    {
                        SoundSystem.instance.SearchSound(1, "");
                        activeWord = word;
                        activeWordDisplay = activeWord.display;
                        hasActiveWord = true;
                        word.TypeLetter();
                        StartCoroutine(CameraController.CameraShake(1f, 1f, 0.1f));
                        if (player != null)
                        {
                            Player.CallPlayerTapAnimation();
                        }
                        break;
                    }
                    else
                    {
                        StartCoroutine(CameraController.CameraShake(2f, 2f, 0.1f));
                    }
                }
            }
            if (hasActiveWord && activeWord.WordTyped())
            {
                hasActiveWord = false;
                words.Remove(activeWord);
                CheckWave();
            }
        }
    }

    public void RemoveActiveWord() {
        words.Remove(activeWord);
        enemiesDisplay.Remove(activeWordDisplay);
        hasActiveWord = false;
        activeWord = null;
    }

}
