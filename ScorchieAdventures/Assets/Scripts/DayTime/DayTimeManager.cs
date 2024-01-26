using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public enum DayTime 
{ 
    MORNING,
    AFTERNOON,
    NIGHT
}

public class DayTimeManager : MonoBehaviour
{
    public DayTime initialDayTime = DayTime.MORNING;
    [SerializeField] public static DayTime currentDayTime;

    public Color nightColor = Color.white;
    public Color afternoonColor = Color.white;

    public delegate void DayTimeHandler(DayTime newDayTime);
    public static event DayTimeHandler OnDayTimeChanged;

    public Animator screenEffects;
    public Light2D globalLight;
    public Image background;

    private void Awake()
    {
        currentDayTime = initialDayTime;
        UpdateDayTime(currentDayTime, true);
    }

    public void UpdateDayTime(DayTime newDayTime, bool isStart) 
    {
        currentDayTime = newDayTime;

        float delay = isStart ? 0f : 0.55f;

        StartCoroutine(UpdateDayTimeWithDelay(delay, isStart));
    }

    private IEnumerator UpdateDayTimeWithDelay(float delay, bool isStart) 
    {
        if (!isStart)
            screenEffects.SetTrigger("DayTime");

        yield return new WaitForSeconds(delay);

        Color newColor = Color.white;

        switch (currentDayTime)
        {
            case DayTime.AFTERNOON:
                newColor = afternoonColor;
                break;
            case DayTime.NIGHT:
                newColor = nightColor;
                break;
            default:
                break;
        }

        globalLight.color = newColor;
        background.color = newColor;

        OnDayTimeChanged?.Invoke(currentDayTime);
    }
}
