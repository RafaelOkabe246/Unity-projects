using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScreenStack : MonoBehaviour
{
    public List<ActivatableUI> stack;

    private ActivatableUI currentScreen;
    public static ScreenStack instance;

    private UI_State currentUIState;

    private void Awake()
    {
        instance = this;

        gameObject.AddComponent<UIStateManager>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Back") && stack.Count > 0 && currentScreen != null)
        {
            if (currentScreen.isBackHandler)
            {
                SoundManager.instance.PlayAudio(AudiosReference.buttonHover2, AudioType.UI, null);

                if (currentScreen.removeParentOnClose)
                    RemoveScreenFromStack(currentScreen.parentUI);
                RemoveScreenFromStack(currentScreen);
            }
        }

        //Bug fixing
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {
            UpdateCurrentScreen();
        }
    }

    public void AddScreenOntoStack(ActivatableUI screen)
    {
        if (!stack.Contains(screen))
        {
            if (FocusableButtonsManager.Instance.buttonOnFocus != null)
                FocusableButtonsManager.Instance.ResetButtonOnFocus();

            stack.Add(screen);

            screen.gameObject.SetActive(true);
            screen.StartOpenCloseAnimation();

            UpdateCurrentScreen();
        }
    }

    public void RemoveScreenFromStack(ActivatableUI screen)
    {
        StartCoroutine(DeactivateScreen(screen));
    }

    public void ClearScreenStack()
    {
        for (int i = 0; i < stack.Count; i++)
            RemoveScreenFromStack(stack[i]);
        stack.Clear();
    }

    private void UpdateCurrentScreen()
    {
        EventSystem.current.SetSelectedGameObject(null);
        if (stack.Count > 0)
        {
            SetButtonToFocus();

            GameState currentGameState = GameManager.instance.CurrentGameState;
            GameState newGameState = GameState.Paused;
            if (currentGameState == GameState.Gameplay)
                GameManager.instance.SetState(newGameState);
        }
        else
            OnScreenStackCleared();
    }

    private void SetButtonToFocus() 
    {
        if (currentUIState == UI_State.MOUSE)
            return;

        currentScreen = stack[stack.Count - 1];
        FocusableButton buttonToFocus = currentScreen.GetDesiredButtonToFocus();
        if (buttonToFocus)
            EventSystem.current.SetSelectedGameObject(buttonToFocus.gameObject);
    }

    private void OnScreenStackCleared()
    {
        currentScreen = null;
        GameState newGameState = GameState.Gameplay;
        GameManager.instance.SetState(newGameState);
    }

    private IEnumerator DeactivateScreen(ActivatableUI screen)
    {
        screen.StartOpenCloseAnimation();

        yield return new WaitForSeconds(screen.delayToClose);

        FocusableButtonsManager.Instance.ResetButtonOnFocus();

        stack.Remove(screen);
        screen.gameObject.SetActive(false);
        UpdateCurrentScreen();
    }

    public void SetNewUIState(UI_State newUIState) 
    {
        currentUIState = newUIState;
        Cursor.visible = currentUIState == UI_State.MOUSE;
        UpdateCurrentScreen();
    }

    public UI_State GetCurrentUIState() 
    {
        return currentUIState;
    }
}


