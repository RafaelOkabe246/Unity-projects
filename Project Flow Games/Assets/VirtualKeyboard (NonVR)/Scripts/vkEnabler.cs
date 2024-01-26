using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class vkEnabler : MonoBehaviour
{
    private void OnEnable()
    {
        ShowVirtualKeyboard();
    }
    public void ShowVirtualKeyboard(){
		TNVirtualKeyboard.instance.ShowVirtualKeyboard();
		//TNVirtualKeyboard.instance.targetText = gameObject.GetComponent<InputField>();
	}
}
