using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuBackground : MonoBehaviour
{
    [SerializeField]
    private float colorChangeSpeed;

    private Color targetColor;
    private Color currentColor;

    private Image backgroundImage;

    [SerializeField]
    private GameObject backgroundCitiesContainer;
    [SerializeField]
    private Image postProcessingImage;

    private void Awake()
    {
        backgroundImage = GetComponent<Image>();
        currentColor = backgroundImage.color;
        backgroundCitiesContainer.SetActive(true);
    }

    private void OnEnable()
    {
        FocusableButtonsManager.Instance.OnFocusButtonChanged += OnFocusButtonChanged;
    }

    private void OnDisable() 
    {
        FocusableButtonsManager.Instance.OnFocusButtonChanged -= OnFocusButtonChanged;
    }

    private void OnFocusButtonChanged(FocusableButton focusableButton) 
    {
        MainMenuFocusableButton mFocusableButton = focusableButton.GetComponent<MainMenuFocusableButton>();
        
        if (!mFocusableButton)
            return;

        targetColor = mFocusableButton.GetBackgroundColor();
        bool backgroundEnabled = mFocusableButton.GetBackgroundCityEnabled();
        backgroundCitiesContainer.SetActive(backgroundEnabled);
        postProcessingImage.gameObject.SetActive(backgroundEnabled);
    }

    private void Update()
    {
        currentColor = Color.Lerp(currentColor, targetColor, colorChangeSpeed * Time.deltaTime);
        backgroundImage.color = currentColor;
    }
}
