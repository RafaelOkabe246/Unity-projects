using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightManager : MonoBehaviour
{
    private Light2D theLight;

    public float morningIntensity;
    public float afternoonIntensity;
    public float nightIntensity;

    protected void OnEnable()
    {
        theLight = GetComponent<Light2D>();
        DayTimeManager.OnDayTimeChanged += OnDayTimeChanged;
        OnDayTimeChanged(DayTimeManager.currentDayTime);
    }

    protected void OnDayTimeChanged(DayTime dayTime) 
    {
        switch(dayTime)
        {
            case DayTime.AFTERNOON:
                theLight.intensity = afternoonIntensity;
                break;
            case DayTime.NIGHT:
                theLight.intensity = nightIntensity;
                break;
            default:
                theLight.intensity = morningIntensity;
                break;
        }
    }
}
