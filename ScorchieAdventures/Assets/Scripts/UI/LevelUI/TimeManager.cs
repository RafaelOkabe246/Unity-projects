using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Header("Time")]
    [HideInInspector] public string timeText;
    [HideInInspector] public int milliSeconds;
    [HideInInspector] public int seconds;
    [HideInInspector] public int minutes;

    [Space(5)]
    [Header("UI")]
    [SerializeField] private ShowTimeUI showTimeUI;

    private float counterTime;
    private float restoredTime;
    private float stoppedTime;
    private bool timerEnabled = true;

    private void OnEnable()
    {
        StartCoroutine(EnableDelegatesWithDelay());
    }

    private void OnDisable()
    {
        StageBlocksHandler.OnStageBlockTransitionEnded -= ShowTimeUI;
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    private IEnumerator EnableDelegatesWithDelay() 
    {
        yield return new WaitForSeconds(0.001f);

        StageBlocksHandler.OnStageBlockTransitionEnded += ShowTimeUI;
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void Start()
    {
        restoredTime = 0f;
        counterTime = Time.timeSinceLevelLoad - restoredTime;
        ResetTime();
    }

    public void ResetTime() 
    {
        restoredTime = Time.timeSinceLevelLoad;
        stoppedTime = 0f;

        milliSeconds = 0;
        seconds = 0;
        minutes = 0;
    }

    private void Update()
    {
        if (timerEnabled)
        {
            counterTime = Time.timeSinceLevelLoad - restoredTime - stoppedTime;

            minutes = (int)(counterTime / 60f) % 60;
            seconds = (int)(counterTime % 60f);
            milliSeconds = (int)(counterTime * 1000f) % 1000;
        }
        else 
        {
            stoppedTime += Time.deltaTime;
        }
    }

    public void ShowTimeUI() 
    {
        timeText = "";

        if (minutes < 10)
            timeText = "0" + minutes + ":";
        else
            timeText = minutes + ":";

        if (seconds < 10)
            timeText += "0";
        timeText += seconds + ":";

        if (milliSeconds < 10)
            timeText += "0";
        char milliSecondsFirstDigit = milliSeconds.ToString()[0];
        char milliSecondsSecondDigit = ' ';
        if (milliSeconds >= 10)
            milliSecondsSecondDigit = milliSeconds.ToString()[1];

        timeText += (milliSecondsFirstDigit != ' ') ? milliSecondsFirstDigit : "";
        timeText += (milliSecondsSecondDigit != ' ') ? milliSecondsSecondDigit : "";

        showTimeUI.UpdateTimeOnScreen(timeText);
    }

    public void UpdateTimeText() 
    {
        timeText = "";

        if (minutes < 10)
            timeText = "0" + minutes + ":";
        else
            timeText = minutes + ":";

        if (seconds < 10)
            timeText += "0";
        timeText += seconds + ":";

        if (milliSeconds < 10)
            timeText += "0";
        char milliSecondsFirstDigit = milliSeconds.ToString()[0];
        char milliSecondsSecondDigit = milliSeconds.ToString()[1];

        timeText += (milliSecondsFirstDigit != ' ') ? milliSecondsFirstDigit : "";
        timeText += (milliSecondsSecondDigit != ' ') ? milliSecondsSecondDigit : "";
    }

    private void OnGameStateChanged(GameState newGameState) 
    {
        timerEnabled = newGameState == GameState.Gameplay;
    }

}
