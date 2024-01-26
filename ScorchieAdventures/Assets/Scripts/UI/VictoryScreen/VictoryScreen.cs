using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VictoryScreen : MonoBehaviour
{
    public TextMeshProUGUI fruitsQuantityText;
    public TextMeshProUGUI crystalsQuantityText;
    public TextMeshProUGUI finalTimerText;

    private CoinsManager coinsManager;
    private CrystalsManager crystalsManager;
    public TimeManager timeManager;

    private void Awake()
    {
        coinsManager = FindObjectOfType<CoinsManager>();
        crystalsManager = FindObjectOfType<CrystalsManager>();
    }

    private void OnEnable()
    {
        SoundsManager.instance.PlayAudio(AudiosReference.stageCompleted, AudioType.UI, null);
        FillFruitsQuantityText();
        FillCrystalsQuantityText();
        FillTimerText();
    }

    private void FillFruitsQuantityText() 
    {
        StartCoroutine(FillFruitsQuantities());
    }

    private IEnumerator FillFruitsQuantities() 
    {
        for(int i = 0; i <= coinsManager.collectedCoins; i++) 
        {
            yield return new WaitForSeconds(0.025f);
            SoundsManager.instance.PlayAudio(AudiosReference.fruitCollect, AudioType.COLLECTABLE, null);
            fruitsQuantityText.text = "x " + i + "/" + coinsManager.maximumNumberOfCoins;
        }

        fruitsQuantityText.text = "x " + coinsManager.collectedCoins + "/" + coinsManager.maximumNumberOfCoins;

        if (coinsManager.collectedCoins == coinsManager.maximumNumberOfCoins) 
        {
            SoundsManager.instance.PlayAudio(AudiosReference.crystalCollect, AudioType.COLLECTABLE, null);
        }
    }

    private void FillCrystalsQuantityText()
    {
        StartCoroutine(FillCrystalsQuantities());
    }

    private IEnumerator FillCrystalsQuantities()
    {
        for (int i = 0; i <= crystalsManager.collectedCoins; i++)
        {
            yield return new WaitForSeconds(0.2f);
            crystalsQuantityText.text = "x " + i + "/" + crystalsManager.maximumNumberOfCoins;
        }

        crystalsQuantityText.text = "x " + crystalsManager.collectedCoins + "/" + crystalsManager.maximumNumberOfCoins;
    }

    private void FillTimerText() 
    {
        char milliSecondsFirstDigit = timeManager.milliSeconds.ToString()[0];
        char milliSecondsSecondDigit = timeManager.milliSeconds.ToString()[1];

        string millisecondText = "";

        millisecondText += (milliSecondsFirstDigit != ' ') ? milliSecondsFirstDigit : "";
        millisecondText += (milliSecondsSecondDigit != ' ') ? milliSecondsSecondDigit : "";

        timeManager.UpdateTimeText();
        finalTimerText.text = timeManager.timeText;
    }

    public void StartNextLevel() 
    {
        StartCoroutine(StartNextLevelDelay());
    }

    private IEnumerator StartNextLevelDelay() 
    {
        yield return new WaitForSeconds(0.25f);

        LevelLoader.instance.LoadNextLevel();
    }

    public void ReturnToMainMenu() 
    {
        StartCoroutine(ReturnToMainMenuDelay());
    }

    //Returns to the Stage Selection Screen
    private IEnumerator ReturnToMainMenuDelay()
    {
        yield return new WaitForSeconds(0.25f);

        LevelLoader.instance.LoadLevel(1);
    }

    public void SaveGameAfterVictory() 
    {
        int currentSlot = SaveSystem.currentGameSlot;
        GameSlot gameSlotToSave = SaveSystem.gameSlots[currentSlot];
        Stage gameSlotStageInfo = gameSlotToSave.saveSlot.stageInfo[CurrentStage.instance.ID];

        //Enabling the next stage
        if (CurrentStage.instance.ID + 1 < gameSlotToSave.saveSlot.stageInfo.Length)
        {
            if (!gameSlotToSave.saveSlot.stageInfo[CurrentStage.instance.ID + 1].Equals(null))
            {
                if (gameSlotToSave.saveSlot.stageInfo[CurrentStage.instance.ID + 1].stageState == StageState.NOT_VISIBLE)
                    gameSlotToSave.saveSlot.stageInfo[CurrentStage.instance.ID + 1].stageState = StageState.VISIBLE;
            }
        }
        

        Stage currentStageInfo = CurrentStage.instance.currentStageInfo;
        currentStageInfo.ID = CurrentStage.instance.ID;

        currentStageInfo.stageFruitsQuantity = coinsManager.collectedCoins;
        currentStageInfo.stageFruitsMaxQuantity = coinsManager.maximumNumberOfCoins;
        currentStageInfo.stageCrystalsQuantity = crystalsManager.collectedCoins;
        currentStageInfo.stageCrystalsMaxQuantity = crystalsManager.maximumNumberOfCoins;
        currentStageInfo.bestTime = finalTimerText.text;
        currentStageInfo.stageState = StageState.CLEARED;

        if (gameSlotStageInfo.stageFruitsQuantity < currentStageInfo.stageFruitsQuantity)
            gameSlotStageInfo.stageFruitsQuantity = currentStageInfo.stageFruitsQuantity;

        gameSlotStageInfo.stageFruitsMaxQuantity = currentStageInfo.stageFruitsMaxQuantity;

        if (gameSlotStageInfo.stageCrystalsQuantity < currentStageInfo.stageCrystalsQuantity)
            gameSlotStageInfo.stageCrystalsQuantity = currentStageInfo.stageCrystalsQuantity;

        gameSlotStageInfo.stageCrystalsMaxQuantity = currentStageInfo.stageCrystalsMaxQuantity;

        string timeToInt = "" + currentStageInfo.bestTime[0] + currentStageInfo.bestTime[1] + currentStageInfo.bestTime[3] + currentStageInfo.bestTime[4] + currentStageInfo.bestTime[6] + currentStageInfo.bestTime[7];
        string saveSlotTimeToInt = "";
        if (gameSlotStageInfo.bestTime != null)
            saveSlotTimeToInt = "" + gameSlotStageInfo.bestTime[0] + gameSlotStageInfo.bestTime[1] + gameSlotStageInfo.bestTime[3] + gameSlotStageInfo.bestTime[4] + gameSlotStageInfo.bestTime[6] + gameSlotStageInfo.bestTime[7];

        if (saveSlotTimeToInt != "")
        {
            if (int.Parse(timeToInt) < int.Parse(saveSlotTimeToInt))
            {
                gameSlotStageInfo.bestTime = currentStageInfo.bestTime;
            }
        }
        else
            gameSlotStageInfo.bestTime = currentStageInfo.bestTime;

        gameSlotStageInfo.stageState = currentStageInfo.stageState;

        gameSlotToSave.saveSlot.stageInfo[CurrentStage.instance.ID] = gameSlotStageInfo;

        SaveSystem.SaveTheGame(gameSlotToSave);
        StagesData.LoadStagesData(3);
    }
}
