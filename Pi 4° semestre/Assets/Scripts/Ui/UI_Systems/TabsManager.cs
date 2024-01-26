using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabsManager : MonoBehaviour
{
    private ScreenStack screenStack;

    public ActivatableUI[] tabs;
    public int currentTab = 0;
    public float inputDelayBetweenTabs;
    private bool canChangeTab = true;

    private void OnEnable()
    {
        foreach (ActivatableUI aui in tabs)
        {
            aui.gameObject.SetActive(false);
        }
        currentTab = 0;

        screenStack = ScreenStack.instance;
        screenStack.AddScreenOntoStack(tabs[currentTab]);
    }

    void Update()
    {
        if (Input.GetButtonDown("NextTab") && canChangeTab)
        {
            SoundManager.instance.PlayAudio(AudiosReference.buttonClick, AudioType.UI, null);
            screenStack.RemoveScreenFromStack(tabs[currentTab]);
            currentTab++;
            if (currentTab > tabs.Length - 1)
                currentTab = 0;
            screenStack.AddScreenOntoStack(tabs[currentTab]);

            StartCoroutine(ChangeTabDelay());
        }

        if (Input.GetButtonDown("PreviousTab") && canChangeTab)
        {
            SoundManager.instance.PlayAudio(AudiosReference.buttonClick, AudioType.UI, null);
            screenStack.RemoveScreenFromStack(tabs[currentTab]);
            currentTab--;
            if (currentTab < 0)
                currentTab = tabs.Length - 1;
            screenStack.AddScreenOntoStack(tabs[currentTab]);

            StartCoroutine(ChangeTabDelay());
        }
    }

    private IEnumerator ChangeTabDelay()
    {
        canChangeTab = false;

        yield return new WaitForSeconds(inputDelayBetweenTabs);

        canChangeTab = true;
    }
}
