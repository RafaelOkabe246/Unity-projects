using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectorController : MonoBehaviour
{
    public ActivatableUI pauseUI;
    private void Update()
    {
        PressPause();
    }

    private void PressPause()
    {
        if (Input.GetButtonDown("Pause"))
        {
            //Trigger pause
            ScreenStack.instance.AddScreenOntoStack(pauseUI);
        }
    }
}
