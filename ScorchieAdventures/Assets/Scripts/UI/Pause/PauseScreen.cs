using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    private ScreenStack screenStack;

    private ActivatableUI pauseScreen;
    [SerializeField] private ActivatableUI returnToMainMenuQuestionScreen;

    private void Start()
    {
        pauseScreen = GetComponent<ActivatableUI>();
    }

    private void OnEnable()
    {
        SoundsManager.instance.PlayAudio(AudiosReference.openPause, AudioType.UI, null);
        screenStack = ScreenStack.instance;
    }

    public void Resume() 
    {
        StartCoroutine(ResumeDelay());
    }

    private IEnumerator ResumeDelay() 
    {
        yield return new WaitForSeconds(0.5f);

        screenStack.RemoveScreenFromStack(pauseScreen);
    }

    public void RestartStage() 
    {
        StartCoroutine(RestartStageDelay());
    }

    private IEnumerator RestartStageDelay() 
    {
        yield return new WaitForSeconds(0.5f);

        LevelLoader.instance.LoadCurrenttLevel();
    }

    public void OpenExitGameQuestion()
    {
        StartCoroutine(OpenExitGameQuestionDelay());
    }

    private IEnumerator OpenExitGameQuestionDelay() 
    {
        yield return new WaitForSeconds(0.25f);

        screenStack.AddScreenOntoStack(returnToMainMenuQuestionScreen);
    }

    public void CloseExitGameQuestion() 
    {
        StartCoroutine(CloseExitGameQuestionDelay());
    }

    private IEnumerator CloseExitGameQuestionDelay()
    {
        yield return new WaitForSeconds(0.25f);

        screenStack.RemoveScreenFromStack(returnToMainMenuQuestionScreen);
    }

    public void ReturnToStageSelection() 
    {
        StartCoroutine(ReturnToStageSelectionDelay());
    }

    private IEnumerator ReturnToStageSelectionDelay() 
    {
        yield return new WaitForSeconds(0.5f);

        LevelLoader.instance.LoadLevel(1);
    }

    public void ReturnToMainMenu() 
    {
        StartCoroutine(ReturnToMainMenuDelay());
    }

    private IEnumerator ReturnToMainMenuDelay()
    {
        SoundsManager.instance.PlayAudio(AudiosReference.exitPause, AudioType.UI, null);
        yield return new WaitForSeconds(0.5f);

        LevelLoader.instance.LoadLevel(0);
    }
}
