using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public enum GameState
{
    Venceu,
    Perdeu,
    Pause

}

public class MainGameController : MonoBehaviour
{
    // Interionects all the main scripts
    public EventManager _EventManager;
    public DialogueManager _DialogueManager;
    public TripulantesStatusController _TripulantesStatusController;
    public PlayerControl _PlayerControl;
    public InterfaceControl _InterfaceControl;
    public TimeLineManager _TimeLineManager;
   // public SoundManager _SoundManager;

    public int DaysCounting;

    public static GameState _GameState;


    public void EndTurn()
    {
        if (DaysCounting >= 10 && _TripulantesStatusController.EveryoneDied == false)
        {
            //Game over
            _GameState = GameState.Venceu;
            SceneManager.LoadScene("Fim de jogo");
            
        }

        if (DaysCounting <= 10 && _TripulantesStatusController.EveryoneDied == true)
        {
            //Game over
            _GameState = GameState.Perdeu;
            SceneManager.LoadScene("Fim de jogo");

        }

        DaysCounting += 1;

        //Play cutscene
        _TimeLineManager.Director.Play();
    }


}
