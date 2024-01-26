using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndGameInterface : MonoBehaviour
{
    public TextMeshProUGUI EndingMessage;

    void Update()
    {
        if (MainGameController._GameState == GameState.Perdeu)
        {
            EndingMessage.text = "Você perdeu!";
        }
        else if (MainGameController._GameState == GameState.Venceu)
        {
            EndingMessage.text = "Você venceu!";
        }    
    }
}
