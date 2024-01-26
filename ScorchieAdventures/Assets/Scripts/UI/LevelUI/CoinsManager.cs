using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    public FruitsUI fruitsUI;

    private List<CollectableCoin> childCoins;
    [HideInInspector] public int collectedCoins;
    [HideInInspector] public int maximumNumberOfCoins;

    private void Start()
    {
        childCoins = new List<CollectableCoin>();
        for (int i = 0; i < transform.childCount; i++)
        {
            childCoins.Add(transform.GetChild(i).GetComponent<CollectableCoin>());
            childCoins[i].coinsManager = this;
        }

        maximumNumberOfCoins = childCoins.Count;
    }

    public void CollectCoin(int quantity) 
    {
        collectedCoins += quantity;

        if (collectedCoins < 0)
            collectedCoins = 0;

        fruitsUI.UpdateUIInformations(collectedCoins);
    }
}
