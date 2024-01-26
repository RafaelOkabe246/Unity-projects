using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuFocusableButton : FocusableButton
{
    [SerializeField]
    private Color backgroundColor;
    [SerializeField]
    private MainMenuImage mainMenuImage;
    [SerializeField]
    private bool BackgroundCityEnabled = true;

    public Color GetBackgroundColor() 
    {
        return backgroundColor;
    }

    public bool GetBackgroundCityEnabled() 
    {
        return BackgroundCityEnabled;
    }

    public override void OnFocusButtonChanged(FocusableButton newFocusButton)
    {
        base.OnFocusButtonChanged(newFocusButton);

        mainMenuImage.EnableAnimation(this.gameObject == newFocusButton.gameObject);
    }
}
