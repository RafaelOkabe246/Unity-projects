using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneStart : MonoBehaviour
{
    public PlayableDirector TimeLine;
    public bool HasPlayedTimeLine;


    public void SetHasPlayed_true()
    {
        HasPlayedTimeLine = true;
    }

    public void PlayCutscene()
    {
        if (HasPlayedTimeLine == false)
        {
            TimeLine.Play();
        }

    }
}
