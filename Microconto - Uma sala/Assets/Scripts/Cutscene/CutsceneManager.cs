using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CutsceneManager : MonoBehaviour
{
    public UnityEvent PlayShadowCutscene;

    public CharacterEventSystem Events;

    private void Start()
    {
        Events = GameObject.FindGameObjectWithTag(Tags.PlayerEvents).GetComponent<CharacterEventSystem>();
    }

    private void Update()
    {
        if(Events.HasExitHouse == true)
        {
            PlayShadowCutscene.Invoke();
        }
    }

}
