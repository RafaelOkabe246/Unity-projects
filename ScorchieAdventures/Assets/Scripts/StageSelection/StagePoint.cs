using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum StageState 
{ 
    NOT_VISIBLE,
    VISIBLE,
    CLEARED
}

[System.Serializable]
public struct Stage
{
    public int ID;
    public int stageFruitsQuantity;
    public int stageFruitsMaxQuantity;
    public int stageCrystalsQuantity;
    public int stageCrystalsMaxQuantity;
    public string bestTime;
    public StageState stageState;
}

public class StagePoint : MonoBehaviour
{
    public int stageID;
    public StageState stageState; 
    public StageWorldUI stageWorldUI;
    public ActivatableUI stagePrompt;
    private PlayerSelector playerSelector;

    public GameObject dataPanel;
    public GameObject noDataPanel;

    [Header("Next and Previous Stage Point")]
    public StagePoint previousStagePoint;
    public StagePoint nextStagePoint;

    private void Start()
    {
        Stage stageInfo = StagesData.stagesData[SaveSystem.currentGameSlot][stageID];

        if (!stageInfo.Equals(null))
            stageState = stageInfo.stageState;
        else
            stageState = StageState.NOT_VISIBLE;

        if (stageState != StageState.CLEARED)
        {
            dataPanel.SetActive(false);
            noDataPanel.SetActive(true);
        }
        else
        {
            dataPanel.SetActive(true);
            noDataPanel.SetActive(false);

            stageWorldUI.FillInformations(stageInfo.bestTime,
                stageInfo.stageFruitsQuantity.ToString(), stageInfo.stageFruitsMaxQuantity.ToString(),
                stageInfo.stageCrystalsQuantity.ToString(), stageInfo.stageCrystalsMaxQuantity.ToString());
        }
    }

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<StageWorldUI>() != null)
                stageWorldUI = transform.GetChild(i).GetComponent<StageWorldUI>();
        }
    }

    private void OnEnable()
    {

        Stage stageInfo = StagesData.stagesData[SaveSystem.currentGameSlot][stageID];

        if (!stageInfo.Equals(null))
            stageState = stageInfo.stageState;
        else
            stageState = StageState.NOT_VISIBLE;

        if (stageState != StageState.CLEARED)
        {
            dataPanel.SetActive(false);
            noDataPanel.SetActive(true);
        }
        else
        {
            dataPanel.SetActive(true);
            noDataPanel.SetActive(false);

            stageWorldUI.FillInformations(stageInfo.bestTime,
                stageInfo.stageFruitsQuantity.ToString(), stageInfo.stageFruitsMaxQuantity.ToString(),
                stageInfo.stageCrystalsQuantity.ToString(), stageInfo.stageCrystalsMaxQuantity.ToString());
        }
    }

    public void SelectStage()
    {
        //Load the stage level
        OpenStagePrompt();
    }

    private void OpenStagePrompt()
    {
        ScreenStack.instance.AddScreenOntoStack(stagePrompt);
    }

    public void ConfirmStage()
    {
        StartCoroutine(ConfirmStageDelay());
    }

    private IEnumerator ConfirmStageDelay() 
    {
        yield return new WaitForSeconds(0.5f);
        LevelLoader.instance.LoadLevel(2 + stageID);
    }

    public void CancelStage() 
    {
        StartCoroutine(CancelStageDelay());
    }

    private IEnumerator CancelStageDelay()
    {
        yield return new WaitForSeconds(0.5f);

        ScreenStack.instance.RemoveScreenFromStack(stagePrompt);
    }
}
