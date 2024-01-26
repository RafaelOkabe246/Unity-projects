using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class GameplayUi : MonoBehaviour
{
    
    [Header("Levels")]
    public TextMeshProUGUI levelText;
    public Animator levelPanelAnim;

    [Header("Money")]
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI moneyTextShadow;
    public Animator moneyTextAnim;

    public void UpdateMoneyText()
    {
        moneyTextAnim.SetTrigger("Toggle");
        moneyText.text = "Money: " + CoinsManager.instance.currentCoins;
        moneyTextShadow.text = moneyText.text;
    }

    public void UpdateLevelText()
    {
        levelPanelAnim.SetTrigger("Show");
        levelText.text = $"{DifficultManager.instance.levelsIndex + 2} - Stage: {DifficultManager.instance.difficultStageIndex + 1}";
    }
}
