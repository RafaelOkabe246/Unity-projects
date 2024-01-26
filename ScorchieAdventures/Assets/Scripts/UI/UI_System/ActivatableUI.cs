
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatableUI : MonoBehaviour
{
    public FocusableButton buttonToFocus;
    public bool isBackHandler;

    [Space(5)]
    [Header("Optional parameters - Parent")]
    public ActivatableUI parentUI;
    public bool removeParentOnClose;

    [Space(5)]
    [Header("Optional parameters - Animation")]
    public float delayToClose;
    public bool containOpenCloseAnimation;
    public Animator anim;

    private void Start()
    {
        if(containOpenCloseAnimation)
            anim.keepAnimatorControllerStateOnDisable = false;
    }

    public FocusableButton GetDesiredButtonToFocus()
    {
        return buttonToFocus;
    }

    public void StartOpenCloseAnimation()
    {
        if (containOpenCloseAnimation)
        {
            anim.SetTrigger("OpenClose");
        }
    }
    
}

