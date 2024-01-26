using DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : DialogueManager
{
    private Text textHolder;
    [SerializeField]
    private string input;

    [Header("Delay variables")]

    [SerializeField] private float delay;

    private void Awake()
    {
        textHolder = GetComponent<Text>(); 

        StartCoroutine(WriteText(input, textHolder, delay));
    }
}
