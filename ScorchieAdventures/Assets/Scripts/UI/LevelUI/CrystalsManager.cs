using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalsManager : MonoBehaviour
{
    public CrystalsUI crystalsUI;

    private List<CollectableCrystal> childCoins;
    [HideInInspector] public int collectedCoins;
    [HideInInspector] public int maximumNumberOfCoins;

    private void Start()
    {
        childCoins = new List<CollectableCrystal>();
        for (int i = 0; i < transform.childCount; i++)
        {
            childCoins.Add(transform.GetChild(i).GetComponent<CollectableCrystal>());
            childCoins[i].crystalsManager = this;
        }

        maximumNumberOfCoins = childCoins.Count;
    }

    public void CollectCoin(int quantity)
    {
        collectedCoins += quantity;
        if (collectedCoins < 0)
            collectedCoins = 0;
        crystalsUI.UpdateUIInformations(collectedCoins);
    }
}
