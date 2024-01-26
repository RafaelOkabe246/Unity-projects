using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SaveSlotsButton : FocusableButton
{
    public TextMeshProUGUI newGame;

    public GameObject noDataPanel;
    public GameObject dataPanel;
    private bool isEmptySlot;

    public Image progressBar;
    public Image progressBarBase;
    public TextMeshProUGUI percentageText;
    public TextMeshProUGUI percentageShadowText;

    public TextMeshProUGUI fruitsQuantity;
    public TextMeshProUGUI fruitsQuantityShadow;
    public TextMeshProUGUI crystalsQuantity;
    public TextMeshProUGUI crystalsQuantityShadow;

    public TextMeshProUGUI stagesClearedText;
    public TextMeshProUGUI stagesClearedShadowText;

    [HideInInspector] public int slotIndex;

    public ActivatableUI slotQuestionPrompt;

    public void FillSlotButtonInfo(bool emptySlot, float progressBarValue, int collectedFruits, int maxFruits, int collectedCrystals, int maxCrystals, int clearedStages, int maxStages)
    {
        isEmptySlot = emptySlot;
        noDataPanel.SetActive(isEmptySlot);
        dataPanel.SetActive(!isEmptySlot);
        newGame.gameObject.SetActive(isEmptySlot);
        progressBar.gameObject.SetActive(!isEmptySlot);
        progressBarBase.gameObject.SetActive(!isEmptySlot);
        percentageText.gameObject.SetActive(!isEmptySlot);
        percentageShadowText.gameObject.SetActive(!isEmptySlot);

        if (!isEmptySlot)
        {
            progressBar.fillAmount = progressBarValue;
            percentageText.text = "" + (progressBarValue * 100) + "%";
            percentageShadowText.text = percentageText.text;

            fruitsQuantity.text = collectedFruits + "/" + maxFruits;
            fruitsQuantityShadow.text = fruitsQuantity.text;
            crystalsQuantity.text = collectedCrystals + "/" + maxCrystals;
            crystalsQuantityShadow.text = crystalsQuantity.text;

            stagesClearedText.text = clearedStages + "/" + maxStages;
            stagesClearedShadowText.text = stagesClearedText.text;
        }
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
    }

    public override void OnClicked()
    {
        base.OnClicked();

        SaveSystem.SelectGameSlot(slotIndex);
        ScreenStack.instance.AddScreenOntoStack(slotQuestionPrompt);

        SoundsManager.instance.PlayAudio(AudiosReference.buttonClicked, AudioType.BUTTON, null);
    }

    public void CloseSlotQuestionPrompt()
    {
        ScreenStack.instance.RemoveScreenFromStack(slotQuestionPrompt);
    }

    public override void OnFocusButtonChanged(FocusableButton newFocusButton)
    {
        base.OnFocusButtonChanged(newFocusButton);

        if (this.gameObject == newFocusButton.gameObject)
        {
            noDataPanel.SetActive(isEmptySlot);
            dataPanel.SetActive(!isEmptySlot);
            newGame.gameObject.SetActive(isEmptySlot);
            progressBar.gameObject.SetActive(!isEmptySlot);
            progressBarBase.gameObject.SetActive(!isEmptySlot);
            percentageText.gameObject.SetActive(!isEmptySlot);
            percentageShadowText.gameObject.SetActive(!isEmptySlot);
            SaveSystem.SelectGameSlot(slotIndex);
        }
    }
}
