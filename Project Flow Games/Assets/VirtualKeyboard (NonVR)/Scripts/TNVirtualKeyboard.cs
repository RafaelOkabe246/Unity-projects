using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TNVirtualKeyboard : MonoBehaviour
{
	
	public static TNVirtualKeyboard instance;
	
	public char letter;

	public WordInput wordInput;
	
	public GameObject vkCanvas;
	
	//public InputField targetText;
	
	
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
		ShowVirtualKeyboard();
		wordInput = FindObjectOfType<WordInput>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void KeyPress(string k){
		letter = char.Parse(k);
		wordInput.TypeLetter();
		//targetText.text = letter;	
	}
	
	public void Del(){
		//letter = letter.Remove(letter.Length - 1, 1);
		//targetText.text = letter;	
	}
	
	public void ShowVirtualKeyboard(){
		vkCanvas.SetActive(true);
	}
	
	public void HideVirtualKeyboard(){
		vkCanvas.SetActive(false);
	}
}
