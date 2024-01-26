using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public ActivatableUI VictoryScreen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Terminou o level
            //LevelLoader.instance.LoadNextLevel();
            ScreenStack.instance.AddScreenOntoStack(VictoryScreen);
        }
    }

}
