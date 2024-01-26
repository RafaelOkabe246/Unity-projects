using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameMenuActivableUI : ActivatableUI
{
    [Space(5)]
    [SerializeField]
    private string BacktoMenuQuestionText = "Time to go back to the office?";
    public ActivatableUI Credits;

    [Space(1)]
    [SerializeField]
    private string ExitQuestionText = "Giving up already?";
    public QuestionPanel ExitQuestion;

    public void OpenBackToMainMenuQuestion()
    {
        QuestionPanel questionPanel = GameplayUIsContainer.instance.GetQuestionPanel().GetComponent<QuestionPanel>();
        questionPanel.InitQuestion(BacktoMenuQuestionText, BackToMainMenu, CloseStartGameQuestion);
        ScreenStack.instance.AddScreenOntoStack(questionPanel);
    }

    public void BackToMainMenu()
    {
        SceneLoader.instance.LoadLevel(0);
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
    public void CloseStartGameQuestion()
    {

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
