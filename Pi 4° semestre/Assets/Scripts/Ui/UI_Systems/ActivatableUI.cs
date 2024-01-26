
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivatableUI : MonoBehaviour
{
    private FocusableButton buttonToFocus;
    public bool isBackHandler;

    [Space(5)]
    [Header("Navigation")]
    [SerializeField]
    [Tooltip("If it's false, the navigation is made using Up/Down, else it is made using Left/Right")]
    private bool horizontalNavigation;

    [Space(5)]
    [Header("Optional parameters - Parent")]
    public ActivatableUI parentUI;
    public bool removeParentOnClose;

    [Space(5)]
    [Header("Optional parameters - Animation")]
    public float delayToClose;
    public bool containOpenCloseAnimation;
    public Animator anim;

    protected void Awake()
    {
        SetupChildButtons();
    }

    protected virtual void Start()
    {
        if (containOpenCloseAnimation)
            anim.keepAnimatorControllerStateOnDisable = false;
    }

    protected virtual void OnEnable()
    {
        //Checks if the screen is enabled, but still not inside the screen stack
        //Automatically adds the screen to the stack
        StartCoroutine(AutomaticallyAddScreenOntoStack());
    }

    private IEnumerator AutomaticallyAddScreenOntoStack() 
    {
        yield return new WaitForSeconds(0.1f);
        if (!ScreenStack.instance.stack.Contains(this))
        {
            ScreenStack.instance.AddScreenOntoStack(this);
        }
    }

    public FocusableButton GetDesiredButtonToFocus() 
    {
        if (buttonToFocus != null)
            return buttonToFocus;

        for (int i = 0; i < transform.childCount; i++)
        {
            FocusableButton btn = transform.GetChild(i).GetComponent<FocusableButton>();
            if (btn)
            {
                buttonToFocus = btn;
                break;
            }
            else 
            {
                continue;
            }
        }

        return buttonToFocus;
    }

    protected void SetupChildButtons() 
    {
        List<Button> childButtons = new List<Button>();

        for (int i = 0; i < transform.childCount; i++)
        {
            Button btn = transform.GetChild(i).GetComponent<Button>();
            if (btn)
                childButtons.Add(btn);
        }

        for (int i = 0; i < childButtons.Count; i++)
        {
            Button btn = childButtons[i];

            Navigation nav = new Navigation();
            nav.mode = Navigation.Mode.Explicit;
            Button downButton = btn;
            Button upButton = btn;
            if (i < childButtons.Count - 1)
            {
                if (i + 1 < childButtons.Count)
                {
                    downButton = childButtons[i + 1];
                }
            }
            else 
            {
                downButton = childButtons[0];
            }

            if (i > 0) 
            {
                upButton = childButtons[i - 1];
            }
            else
            {
                if(childButtons.Count > 1)
                    upButton = childButtons[childButtons.Count - 1];
            }

            if (!horizontalNavigation)
            {
                if (downButton != btn)
                    nav.selectOnDown = downButton;
                if (upButton != btn)
                    nav.selectOnUp = upButton;
            }
            else 
            {
                if (downButton != btn)
                    nav.selectOnRight = downButton;
                if (upButton != btn)
                    nav.selectOnLeft = upButton;
            }

            btn.navigation = nav;
        }    
    }

    public void StartOpenCloseAnimation()
    {
        if (containOpenCloseAnimation)
        {
            anim.SetTrigger("OpenClose");
        }
    }

}
