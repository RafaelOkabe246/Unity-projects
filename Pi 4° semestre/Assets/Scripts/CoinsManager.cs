using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class CoinsManager : MonoBehaviour
{
    public GameEvent moneyEvent;
    public static CoinsManager instance;
    public int currentCoins;
    public TextMeshProUGUI moneyText;

    private void Awake()
    {
        instance = this;
    }


    public int GetCoins()
    {
        return currentCoins;
    }

    /// <summary>
    /// Called when an enemy is destroyed
    /// </summary>
    public void GainCoins()
    {
        currentCoins++; //Test
        ProgressionManager.instance.currentRunInfo.coins++;
        moneyEvent.Raise(this,currentCoins);
    }

    public void SpendCoins(int value)
    {
        currentCoins -= value;
        moneyEvent.Raise(this, currentCoins);
    }


}
