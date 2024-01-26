using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParticles : MonoBehaviour
{
    public ParticleSystem leafParticle;
    public ParticleSystem fireflyParticle;

    protected void OnEnable()
    {
        DayTimeManager.OnDayTimeChanged += OnDayTimeChanged;
        OnDayTimeChanged(DayTimeManager.currentDayTime);
    }

    protected void OnDayTimeChanged(DayTime dayTime)
    {
        if (leafParticle == null)
        {
            leafParticle = GameObject.Find("LeafParticle").GetComponent<ParticleSystem>();
        }
        
        if(fireflyParticle == null) 
        {
            fireflyParticle = GameObject.Find("FireflyParticle").GetComponent<ParticleSystem>();
        }

        switch (dayTime)
        {
            case DayTime.MORNING:
                leafParticle.Play();
                fireflyParticle.Stop();
                break;
            case DayTime.AFTERNOON:
                leafParticle.Play();
                fireflyParticle.Stop();
                break;
            case DayTime.NIGHT:
                leafParticle.Stop();
                fireflyParticle.Play();
                break;
            default:
                leafParticle.Play();
                fireflyParticle.Stop();
                break;
        }
    }
}
