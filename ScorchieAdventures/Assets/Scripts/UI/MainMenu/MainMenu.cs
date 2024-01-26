using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private ScreenStack screenStack;

    public ActivatableUI saveSlotScreen;
    public ActivatableUI slotQuestionScreen;
    public ActivatableUI deleteSlotQuestionScreen;
    public ActivatableUI creditsScreen;
    public ActivatableUI settingsScreen;
    public ActivatableUI exitQuestionScreen;

    private void OnEnable()
    {
        screenStack = ScreenStack.instance;
    }

    public void PlayTheGame() 
    {
        StartCoroutine(PlayGameDelay());
    }

    private IEnumerator PlayGameDelay() 
    {
        yield return new WaitForSeconds(0.5f);
        LevelLoader.instance.LoadNextLevel();
    }

    public void OpenSaveSlotsScreen() 
    {
        StartCoroutine(OpenSaveSlotScreenDelay());
    }

    private IEnumerator OpenSaveSlotScreenDelay()
    {
        yield return new WaitForSeconds(0.5f);
        screenStack.AddScreenOntoStack(saveSlotScreen);
    }

    public void CloseSlotQuestionScreen()
    {
        StartCoroutine(CloseSlotQuestionScreenDelay());
    }

    private IEnumerator CloseSlotQuestionScreenDelay()
    {
        yield return new WaitForSeconds(0.5f);
        screenStack.RemoveScreenFromStack(slotQuestionScreen);
    }

    public void CloseDeleteSlotQuestionScreen() 
    {
        StartCoroutine(CloseDeleteSlotQuestionScreenDelay());
    }

    private IEnumerator CloseDeleteSlotQuestionScreenDelay() 
    {
        yield return new WaitForSeconds(0.5f);
        screenStack.RemoveScreenFromStack(deleteSlotQuestionScreen);
    }

    public void OpenCreditsScreen() 
    {
        screenStack.AddScreenOntoStack(creditsScreen);
    }
    public void OpenSettingsScreen()
    {
        screenStack.AddScreenOntoStack(settingsScreen);
    }

    public void OpenExitGameQuestion()
    {
        StartCoroutine(OpenExitGameQuestionDelay());
    }

    private IEnumerator OpenExitGameQuestionDelay() 
    {
        yield return new WaitForSeconds(0.25f);
        screenStack.AddScreenOntoStack(exitQuestionScreen);
    }

    public void ExitGame() 
    {
        StartCoroutine(ExitGameDelay());
    }

    private IEnumerator ExitGameDelay()
    {
        yield return new WaitForSeconds(0.25f);
        Application.Quit();
    }

    public void DoNotExitGame() 
    {
        StartCoroutine(DoNotExitGameDelay());
    }

    private IEnumerator DoNotExitGameDelay()
    {
        yield return new WaitForSeconds(0.25f);
        screenStack.RemoveScreenFromStack(exitQuestionScreen);
    }

}
