using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public float TimerCounter;
    public float BeginTimer;

    private void Start()
    {
        TimerCounter = BeginTimer;
    }

    private void Update()
    {
        if (TimerCounter > 0)
        {
            TimerCounter -= Time.deltaTime;
        }
        else if(TimerCounter <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }


}
