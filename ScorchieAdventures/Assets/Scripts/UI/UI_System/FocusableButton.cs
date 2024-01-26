using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * Focusable Buttons are the buttons we should use alongside the ScreenStack system. They contain a delegate that is responsible for detecting if the focused button has changed;
*/
public class FocusableButton : MonoBehaviour, ISelectHandler
{
    public Animator anim;
    [Tooltip("The buttonName variable is completely optional. In general cases, it should be empty")]
    public string buttonName;
    [HideInInspector] public bool isEnabled;

    private void OnEnable()
    {
        FocusableButtonsManager.Instance.OnFocusButtonChanged += OnFocusButtonChanged;
    }

    private void OnDisable()
    {
        FocusableButtonsManager.Instance.OnFocusButtonChanged -= OnFocusButtonChanged;
    }

    public virtual void OnSelect(BaseEventData eventData)
    {
        FocusableButtonsManager.Instance.OnChangeFocusButton(this);
    }

    public virtual void OnClicked() 
    {
        isEnabled = true;
        if (anim != null)
        {
            anim.SetBool(buttonName + "Enabled", true);
            anim.SetBool(buttonName + "Disabled", false);
            SoundsManager.instance.PlayAudio(AudiosReference.buttonClicked, AudioType.BUTTON, null);
            anim.SetTrigger("Clicked");
        }
    }

    public virtual void OnFocusButtonChanged(FocusableButton newFocusButton) 
    {
        if (this.gameObject == newFocusButton.gameObject && !isEnabled)
        {
            isEnabled = true;
            if (anim != null)
            {
                anim.SetBool(buttonName + "Enabled", true);
                anim.SetBool(buttonName + "Disabled", false);
            }
            SoundsManager.instance.PlayAudio(AudiosReference.fruitCollect, AudioType.COLLECTABLE, SoundsManager.instance.secondSfxSource);
        }
        else
        {
            isEnabled = false;
            if (anim != null)
            {
                anim.SetBool(buttonName + "Enabled", false);
                anim.SetBool(buttonName + "Disabled", true);
            }
        }
    }

    public void DisableButtonAnimation() 
    {
        isEnabled = false;
        if (anim != null)
        {
            anim.SetBool(buttonName + "Enabled", false);
            anim.SetBool(buttonName + "Disabled", true);
        }
    }

}
