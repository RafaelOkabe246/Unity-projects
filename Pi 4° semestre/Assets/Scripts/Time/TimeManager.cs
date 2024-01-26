using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    private float slowdownLength = 2f;
    private bool sleeping;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (!sleeping && Time.timeScale != 1f)
        {
            Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            Time.fixedDeltaTime = Time.timeScale * .02f;
        }
    }

    public void CallSlowMotion(float slowdownFactor, float newSlowdownLength) 
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
        slowdownLength = newSlowdownLength;
    }

    public void StopSlowMotion() 
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = .02f;
        StopCoroutine(PerformSleep(0f));
        slowdownLength = 2f;
    }

    public void Sleep(float duration)
    {
        StopCoroutine(nameof(PerformSleep));
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale * .02f;
        StartCoroutine(nameof(PerformSleep), duration);
    }

    private IEnumerator PerformSleep(float duration)
    {
        sleeping = true;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(duration);
        sleeping = false;
        Time.timeScale = 1;
    }

    public void SleepWithDelay(float duration, float delay) 
    {
        StartCoroutine(DelayBeforeSleep(duration, delay));
    }

    private IEnumerator DelayBeforeSleep(float duration, float delay) 
    {
        yield return new WaitForSeconds(delay);

        Sleep(duration);
    }
}
