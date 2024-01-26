using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEventSystem : MonoBehaviour
{
    public bool HasKey;
    public bool HasExitHouse;
    public bool HasFollowShadownGirl;

    public static CharacterEventSystem Instance;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        //Here we load the data in the Event manager to the player
        EventManager data = SaveSystem.LoadPlayer();

        HasKey = data.HasKey;
        HasExitHouse = data.HasExitHouse;
        HasFollowShadownGirl = data.HasFollowShadownGirl;
    }

}
