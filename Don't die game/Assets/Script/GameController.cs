using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class GameController : MonoBehaviour
{
    public bool isFinished;

    public float currentTime;
    public float StartcurrentTime;
    public float EndcurrentTime = 100f;


    void Start()
    {
        isFinished = false;
        StartcurrentTime = currentTime;
        StartcurrentTime = 0f;

    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime >= EndcurrentTime)
        {
            isFinished = true;
        }
    }

    public void Fim_da_demo()
    {
        SceneManager.LoadScene("Credits");
    }

}
