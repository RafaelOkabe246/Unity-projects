using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStates
{
    Pause,
    Gameplay
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public SlotManager slotManager;
    public bool hasWonMinigame;
    public delegate void OnChangeGameState();
    public OnChangeGameState onChangeGameState;
    public bool hasWonGame;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        hasWonMinigame = true;
    }


    public SlotManager CheckSlotManager()
    {
        if (slotManager == null)
        {
            slotManager = FindObjectOfType<SlotManager>();
        }
        return slotManager;
    }

    public void ChangeGameState()
    {

    }

    

}
