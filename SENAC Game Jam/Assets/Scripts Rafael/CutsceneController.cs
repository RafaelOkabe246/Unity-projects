using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneController : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public PlayableAsset introductionCutscene;
    public PlayableAsset gansoTriggerCutscene;
    

    public void SetCutscene(PlayableAsset selectedCutscene)
    {
        playableDirector.playableAsset = selectedCutscene;
        playableDirector.Play();
    }
}
