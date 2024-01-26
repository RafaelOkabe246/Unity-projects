using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Paused,
    Lose,
    Gameplay,
    Start
}
public class GameManager : MonoBehaviour
{
    public GameState currentState;
    public Bird bird;
    public static GameManager instance;
    public UiManager uiManager;
    public int points;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartCoroutine(Countdown(3));
    }

    IEnumerator Countdown(int seconds)
    {
        ChangeState(GameState.Start);

        bird.enabled = false;
        bird.GetComponent<Rigidbody2D>().gravityScale = 0;

        int count = seconds;

        while (count > 0)
        {
            uiManager.counterText.text = count.ToString();
            // display something...
            yield return new WaitForSeconds(1);
            count--;
        }


        // count down is finished...
        StartGame();
    }

    void StartGame()
    {
        bird.enabled = true;
        bird.GetComponent<Rigidbody2D>().gravityScale = 1;
        ChangeState(GameState.Gameplay);
    }

    public void AddPoints()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.pointClip);
        points++;
        uiManager.UpdatePoints();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetPause(bool i)
    {
        if(i)
            currentState = GameState.Paused;
        else
            currentState = GameState.Gameplay;

        uiManager.ChangeCanvas(currentState);
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
        uiManager.ChangeCanvas(currentState);
    }
}
