using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSlotsUI : MonoBehaviour
{
    public Transform slotButtonsSizeBox;
    [HideInInspector] public List<SaveSlotsButton> buttons;
    public ActivatableUI deleteSlotQuestionScreen;

    private void Awake()
    {
        for (int i = 0; i < slotButtonsSizeBox.childCount; i++) 
        {
            SaveSlotsButton slotButton = slotButtonsSizeBox.GetChild(i).GetComponent<SaveSlotsButton>();
            buttons.Add(slotButton);

            LoadSlots(i);
        }
    }

    private void LoadSlots(int i)
    {
        GameSlot gameSlot = SaveSystem.LoadTheGame(i);
        if (gameSlot != null)
        {
            float progressBarValue = GetClearedStagesPercentage(gameSlot);
            int collectedFruits = GetAllCollectedAmountOfFruits(gameSlot);
            int maxAmountOfFruits = GetMaxAmountOfFruits(gameSlot);
            int collectedCrystals = GetAllCollectedAmountOfCrystals(gameSlot);
            int maxAmountOfCrystals = GetMaxAmountOfCrystals(gameSlot);
            int clearedStages = GetAllClearedStages(gameSlot);
            int maxStages = GetAllStages(gameSlot);
            buttons[i].FillSlotButtonInfo(false, progressBarValue, collectedFruits, maxAmountOfFruits, collectedCrystals, maxAmountOfCrystals, clearedStages, maxStages);
        }
        else
        {
            buttons[i].FillSlotButtonInfo(true, 0, 0, 0, 0, 0, 0, 0);
        }

        buttons[i].slotIndex = i;
    }

    private float GetClearedStagesPercentage(GameSlot gameSlot) 
    {
        float clearedStages = 0;
        float totalStages = gameSlot.saveSlot.stageInfo.Length;

        for(int i = 0; i < totalStages; i++) 
        {
            if(gameSlot.saveSlot.stageInfo[i].stageState == StageState.CLEARED)
                clearedStages++;
        }

        float percentage = (clearedStages / totalStages);

        return percentage;
    }

    private int GetAllClearedStages(GameSlot gameSlot) 
    {
        int clearedStages = 0;
        int totalStages = gameSlot.saveSlot.stageInfo.Length;

        for (int i = 0; i < totalStages; i++)
        {
            if (gameSlot.saveSlot.stageInfo[i].stageState == StageState.CLEARED)
                clearedStages++;
        }

        return clearedStages;
    }

    private int GetAllStages(GameSlot gameSlot)
    {
        int totalStages = gameSlot.saveSlot.stageInfo.Length;

        return totalStages;
    }

    private int GetMaxAmountOfFruits(GameSlot gameSlot) 
    {
        int maxFruits = 0;
        int totalStages = gameSlot.saveSlot.stageInfo.Length;
        for (int i = 0; i < totalStages; i++) 
        {
            maxFruits += gameSlot.saveSlot.stageInfo[i].stageFruitsMaxQuantity;
        }

        return maxFruits;
    }

    private int GetAllCollectedAmountOfFruits(GameSlot gameSlot) 
    {
        int collectedFruits = 0;
        int totalStages = gameSlot.saveSlot.stageInfo.Length;
        for (int i = 0; i < totalStages; i++)
        {
            collectedFruits += gameSlot.saveSlot.stageInfo[i].stageFruitsQuantity;
        }

        return collectedFruits;

    }

    private int GetMaxAmountOfCrystals(GameSlot gameSlot)
    {
        int maxCrystals = 0;
        int totalStages = gameSlot.saveSlot.stageInfo.Length;
        for (int i = 0; i < totalStages; i++)
        {
            maxCrystals += gameSlot.saveSlot.stageInfo[i].stageCrystalsMaxQuantity;
        }

        return maxCrystals;
    }

    private int GetAllCollectedAmountOfCrystals(GameSlot gameSlot)
    {
        int collectedCrystals = 0;
        int totalStages = gameSlot.saveSlot.stageInfo.Length;
        for (int i = 0; i < totalStages; i++)
        {
            collectedCrystals += gameSlot.saveSlot.stageInfo[i].stageCrystalsQuantity;
        }

        return collectedCrystals;

    }

    #region DELETE GAME SLOT
    private void Update()
    {
        DeleteGameSlotInput();
    }

    private void DeleteGameSlotInput() 
    {
        if (Input.GetButtonDown("DeleteSlot")) 
        {
            int currentSlotIndex = SaveSystem.currentGameSlot;
            GameSlot currentGameSlot = SaveSystem.LoadTheGame(currentSlotIndex);
            if (currentGameSlot != null) 
            {
                ScreenStack.instance.AddScreenOntoStack(deleteSlotQuestionScreen);
            }
        }
    }

    public void OnDeleteSlot() 
    {
        int currentSlotIndex = SaveSystem.currentGameSlot;
        SaveSystem.DeleteGameSlot(currentSlotIndex);
        LoadSlots(currentSlotIndex);
        ScreenStack.instance.UpdateCurrentScreen();
        LevelLoader.instance.LoadCurrenttLevel();
    }
    #endregion
}
