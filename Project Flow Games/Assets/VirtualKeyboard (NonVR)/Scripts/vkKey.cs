using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vkKey : MonoBehaviour
{
	
	public string k = "xyz";
	
	public void KeyClick(){
		TNVirtualKeyboard.instance.KeyPress(k);
	}
}
