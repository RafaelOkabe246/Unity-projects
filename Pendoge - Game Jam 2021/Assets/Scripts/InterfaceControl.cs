using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InterfaceControl : MonoBehaviour
{
    public MainGameController GameController;
    
    public TextMeshProUGUI DaysCounting;

    private void Update()
    {
        DaysCounting.text = "Dia " + GameController.DaysCounting;
    }

   
}
