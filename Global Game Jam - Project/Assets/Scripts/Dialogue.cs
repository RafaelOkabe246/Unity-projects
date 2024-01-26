using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] internal TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float Tippingspeed;

    public GameObject TextBackground;
    public GameObject continueButton;


    private void Start()
    {
        //TextBackground.gameObject.SetActive(false);
        //textDisplay.gameObject.SetActive(false);
        //continueButton.gameObject.SetActive(false);
    }

    public void StartType()
    {
        //TextBackground.gameObject.SetActive(true);
        //textDisplay.gameObject.SetActive(true);
        //continueButton.gameObject.SetActive(true);
        StartCoroutine(Type());
    }

    private void Update()
    {
        if (textDisplay.text == sentences[index])
        {
            continueButton.SetActive(true);
        }
    }


    IEnumerator Type() 
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(Tippingspeed);

        }
    }

    public void NextSentence()
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
            textDisplay.text = "";
            continueButton.SetActive(false);
        }
    }

}
