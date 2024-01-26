using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{

    public void PlayCustscene(PlayableDirector cutscene_to_play)
    {
        cutscene_to_play.Play();
    }
}
