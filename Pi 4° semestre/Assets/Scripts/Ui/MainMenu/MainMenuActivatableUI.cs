using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuActivatableUI : ActivatableUI
{
    [Space(5)]
    [SerializeField]
    private string ContinueSaveText = "Continue from old save?";
    [SerializeField]
    private string StartQuestionText = "Time to beat'em up!?";
    [Header("Main Menu screens")]
    public ActivatableUI Credits;
    public ActivatableUI Settings;

    [Space(1)]
    [SerializeField]
    private string ExitQuestionText = "Giving up already?";
    public QuestionPanel ExitQuestion;

    public void OpenStartGameQuestion() 
    {
        QuestionPanel questionPanel = GameplayUIsContainer.instance.GetQuestionPanel().GetComponent<QuestionPanel>();
        if(SaveSystem.LoadGame() != null)
            questionPanel.InitQuestion(StartQuestionText, ContinueFromSave, CloseStartGameQuestion);
        else
            questionPanel.InitQuestion(StartQuestionText, StartGame, CloseStartGameQuestion);

        ScreenStack.instance.AddScreenOntoStack(questionPanel);
    }

    public void ContinueFromSave()
    {
        QuestionPanel questionPanelSave = GameplayUIsContainer.instance.GetQuestionPanelContinueGame().GetComponent<QuestionPanel>();
        questionPanelSave.InitQuestion(ContinueSaveText, StartGame, StartGameDeleteSave);
        ScreenStack.instance.AddScreenOntoStack(questionPanelSave);
    }

    void StartGameDeleteSave()
    {
        SaveSystem.DeleteGameSave();
        StartGame();
    }

    public void StartGame() 
    {
        SceneLoader.instance.LoadNextLevel();
    }

    public void CloseStartGameQuestion()
    {

    }

    public void OpenCredits() 
    {
        StartCoroutine(DelayToOpenScreen(Credits));
    }

    private IEnumerator DelayToOpenScreen(ActivatableUI screenToOpen) 
    {
        yield return new WaitForSeconds(0.4f);

        ScreenStack.instance.AddScreenOntoStack(screenToOpen);
    }

    public void OpenSettings()
    {
        StartCoroutine(DelayToOpenScreen(Settings));
    }

    public void OpenExitQuestion()
    {
        ScreenStack.instance.AddScreenOntoStack(ExitQuestion);

        QuestionPanel questionPanel = ExitQuestion.GetComponent<QuestionPanel>();
        if (questionPanel)
            questionPanel.InitQuestion(ExitQuestionText, ExitGame, CloseExitQuestion);
        else 
        {
            Debug.LogError("Exit Question: The ExitQuestion activatable UI isn't of type QuestionPanel!");
        }
    }

    public void CloseExitQuestion() 
    {

    }

    public void ExitGame() 
    {
        Application.Quit();
    }
}
