using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayUIsContainer : MonoBehaviour
{
    public static GameplayUIsContainer instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] 
    private ActivatableUI pauseScreen;

    [SerializeField]
    private ActivatableUI victoryScreen;

    [SerializeField]
    private ActivatableUI defeatScreen;

    [SerializeField]
    private ActivatableUI questionPanel;

    [SerializeField]
    private ActivatableUI questionPanelSave;

    [SerializeField]
    private ActivatableUI shopPanel;

    [SerializeField]
    private ActivatableUI dialoguePanel;

    public ActivatableUI GetPauseScreen() 
    {
        return pauseScreen;
    }

    public ActivatableUI GetVictoryScreen()
    {
        return victoryScreen;
    }

    public ActivatableUI GetDefeatScreen()
    {
        return defeatScreen;
    }

    public ActivatableUI GetQuestionPanel()
    {
        return questionPanel;
    }

    public ActivatableUI GetQuestionPanelContinueGame()
    {
        return questionPanelSave;
    }

    public ActivatableUI GetShopScreen()
    {
        return shopPanel;
    }

    public ActivatableUI GetDialogueScreen()
    {
        return dialoguePanel;
    }
}
