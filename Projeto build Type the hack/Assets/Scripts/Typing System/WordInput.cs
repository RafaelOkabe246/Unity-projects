using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Letter typing system
*/
public class WordInput : MonoBehaviour
{
    public static bool canType;
    public WordManager wordManager;

    private void Start()
    {
        canType = true;
    }

    void Update()
    {

        foreach (char letter in Input.inputString)
        {
            if(canType)
               wordManager.TypeLetter(letter);
        }
    }

    //When customizing words
    public void CanTypeWords()
    {
        canType = true;
    }

    public void CannotTypeWords()
    {
        canType = false;
    }
}
