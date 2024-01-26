using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class startCutscene : MonoBehaviour
{
    public PlayableDirector Cutscene;


    public void Play()
    {
        Cutscene.Play();
    }
}
