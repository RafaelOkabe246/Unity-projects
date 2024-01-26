using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;


public class CutsceneTrigger : MonoBehaviour
{
    public CutsceneManager cutsceneManager;
    public PlayableDirector cutscene;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            cutsceneManager.PlayCustscene(cutscene);
        }
    }

}
