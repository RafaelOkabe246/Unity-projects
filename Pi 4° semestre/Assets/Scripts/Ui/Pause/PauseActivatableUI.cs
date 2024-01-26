using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseActivatableUI : ActivatableUI
{
    [Header("Pause menu")]
    public ActivatableUI Options;

    public void ResumeGame() 
    {
        ScreenStack.instance.RemoveScreenFromStack(this);
    }

    public void ShowRestartQuestion() 
    {
        QuestionPanel questionPanel = GameplayUIsContainer.instance.GetQuestionPanel().GetComponent<QuestionPanel>();
        questionPanel.InitQuestion("Restart?", Restart, CloseQuestionPanel);
        ScreenStack.instance.AddScreenOntoStack(questionPanel);
    }

    public void Restart() 
    {
        SceneLoader.instance.LoadCurrenttLevel();
    }

    public void CloseQuestionPanel() 
    { 
        
    }

    public void SetOptions(bool i)
    {
        if (i)
            ScreenStack.instance.AddScreenOntoStack(Options);
        else
            ScreenStack.instance.RemoveScreenFromStack(Options);
    }

    public void OpenMainMenu()
    {
        SceneLoader.instance.LoadLevel(0);
    }

    public void ShowExitQuestion()
    {
        QuestionPanel questionPanel = GameplayUIsContainer.instance.GetQuestionPanel().GetComponent<QuestionPanel>();
        questionPanel.InitQuestion("Giving up already?", Exit, CloseQuestionPanel);
        ScreenStack.instance.AddScreenOntoStack(questionPanel);
    }

    public void Exit()
    {
        OpenMainMenu();
    }
}
