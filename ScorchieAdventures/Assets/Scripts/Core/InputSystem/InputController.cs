using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static InputController instance;

    private void Awake()
    {
        instance = this;
        FillDictionary();
    }

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
}
