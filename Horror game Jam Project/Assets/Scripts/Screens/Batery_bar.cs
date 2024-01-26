using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Batery_bar : MonoBehaviour
{
    public Slider Batterybar;
    public float maxBattery;
    public float currentBatery;
    public static Batery_bar instance;

    public static bool Can_Light;

    private float _Amount;
    public static bool Using_battery;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        maxBattery = 100;
        currentBatery = maxBattery;
        Batterybar.maxValue = maxBattery;
        Batterybar.value = maxBattery;
        Can_Light = true;
    }

    void Update()
    {
        IsUsingBattery();
        if (currentBatery <= 0)
        {
            Can_Light = false;
        }
    }

    public void UseLantern(float amount)
    {
        _Amount = amount;

        if(currentBatery - amount >= 0)
        {
            //Batterybar.value = currentBatery;
            //currentBatery += -amount * Time.deltaTime;
            Using_battery = true;
           // StartCoroutine(Decrasing_Battery());
        }

    }

    private IEnumerator Decrasing_Battery()
    {
        yield return new WaitForSeconds(1);

        while (Input.GetKeyDown(KeyCode.Space))
        {
            currentBatery += _Amount;
            currentBatery -= maxBattery / 100;
            Batterybar.value = currentBatery;
            yield return new WaitForSeconds(0.1f);
        }
    }

    void IsUsingBattery()
    {
        if(Using_battery == true)
        {
            Batterybar.value = currentBatery;
            currentBatery += -_Amount * Time.deltaTime;
        }
        else
        {
            Batterybar.value = currentBatery;
        }

    }
}
