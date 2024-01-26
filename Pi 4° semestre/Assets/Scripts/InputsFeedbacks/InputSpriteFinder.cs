using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSpriteFinder : MonoBehaviour
{
    public static InputSpriteFinder instance;


    [System.Serializable]
    public struct Inputs 
    {
        public KeyCode keyCode;
        public Sprite[] sprites;
    }
    public List<Inputs> inputs;

    public Dictionary<KeyCode, Sprite[]> inputsDictionary;

    private void FillDictionary()
    {
        inputsDictionary = new Dictionary<KeyCode, Sprite[]>();

        foreach (Inputs input in inputs)
        {
            inputsDictionary.Add(input.keyCode, input.sprites);
        }
    }

    private void Awake() 
    {
        instance = this;
        FillDictionary();
    }

    public Sprite[] FindInputSpritesFromKeyCode(KeyCode key) 
    {
        Sprite[] spr;

        foreach (KeyCode k in inputsDictionary.Keys) 
        {
            if (k == key) 
            {
                spr = inputsDictionary[key];
                return spr;
            }
        }

        return null;
    }

}
