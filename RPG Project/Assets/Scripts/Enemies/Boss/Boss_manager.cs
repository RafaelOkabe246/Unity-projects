using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class Boss_manager : MonoBehaviour
{
    public Scene_controller Scene;

    private Boss _Boss;

    //Events 
    public PlayableDirector Lost_cutscene;
    public PlayableDirector Begin_cutscene;

    void Start()
    {
        _Boss = FindObjectOfType(typeof(Boss)) as Boss;
    }

    //Boss defeat events
    public void Boss_defeated()
    {
        if(_Boss.Defeated == true)
        {
            //Lost_cutscene.Play();
            Scene.Next_level();
        }
    }

    //Boss opening events
    public void Boss_opening()
    {
        Begin_cutscene.Play();
    }


    //Starting battle
    public void Start_battle()
    {
        _Boss.isFighting = true;

    }
}
