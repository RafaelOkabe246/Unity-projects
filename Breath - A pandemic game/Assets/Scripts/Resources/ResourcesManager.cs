using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    public int money;
    public int supply;

    public static ResourcesManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void SetSupply(object value)
    {
        supply += (int)value;
    }
    public void SetMoney(object value)
    {
        money += (int)value;
    }
}
