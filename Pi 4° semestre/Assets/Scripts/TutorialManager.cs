using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;
    public bool isTutorial;
    public GameEvent triggerTutorialEvent;
    public GameEvent endTutorialEvent;

    private void Awake()
    {
        instance = this;
    }

    public void EndTutorial()
    {
        isTutorial = false;
        endTutorialEvent.Raise(this, "");
    }

    public void TriggerTutorial()
    {
        isTutorial = true;
        triggerTutorialEvent.Raise(this, "");
    }
}
