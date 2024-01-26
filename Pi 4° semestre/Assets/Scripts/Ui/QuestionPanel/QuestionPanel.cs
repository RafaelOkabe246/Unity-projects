using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class QuestionPanel : ActivatableUI
{
    [Space(5)]
    [Header("Question properties")]

    [SerializeField]
    private TextMeshProUGUI questionText;
    [SerializeField]
    private TextMeshProUGUI questionTextShadow;
    [SerializeField]
    private Button yesButton;
    [SerializeField]
    private Button noButton;

    public void InitQuestion(string Question, UnityAction Yes_Function, UnityAction No_Function) 
    {
        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();

        questionText.text = Question;
        questionTextShadow.text = questionText.text;

        yesButton.onClick.AddListener(Yes_Function);
        yesButton.onClick.AddListener(OnAnswerSelected);
        noButton.onClick.AddListener(No_Function);
        noButton.onClick.AddListener(OnAnswerSelected);
    }

    private void OnAnswerSelected() 
    {
        ScreenStack.instance.RemoveScreenFromStack(this);
    }
}
