using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimeLineManager : MonoBehaviour
{
    private GameController _GameController;

    public PlayableDirector playableDirectors;

    void Start()
    {
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
    }


     void Update()
    {
        if(_GameController.isFinished == true) 
        {
            playableDirectors.Play();
        }
    }


}
