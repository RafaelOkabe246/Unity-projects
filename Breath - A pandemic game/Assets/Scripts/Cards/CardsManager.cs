using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardsManager : MonoBehaviour
{
    public PlayerActions playerActions;
    public static CardsManager instance;

    public Stack<Card> cards;

    [SerializeField] private Card currentCard;
    [SerializeField] Choice currentChoice;

    [Header("Ui card")]
    public TextMeshProUGUI choice1Text;
    public TextMeshProUGUI choice2Text;
    public TextMeshProUGUI characterNameText;


    private void Awake()
    {
        instance = this;
        playerActions = PlayerActions.instance;
    }

    private void OnEnable()
    {
        playerActions.dirNavigation += HooverCardOptions;
        playerActions.select += SelectOption;
    }
    private void OnDisable()
    {
        playerActions.dirNavigation -= HooverCardOptions;
        playerActions.select -= SelectOption;

    }

    void SetNewCard()
    {
        currentCard = cards.Peek();

        //Ui info
        choice1Text.text = currentCard.choice1.choiceText;
        choice2Text.text = currentCard.choice2.choiceText;
        characterNameText.text = currentCard.characterName;
        //characterImage.sprite = currentCard.

        currentChoice = new Choice();
    }

    public void CloseCard()
    {
        cards.Pop();
        //Update ui
        SetNewCard();
    }


    public void HooverCardOptions(int dir)
    {
        Debug.Log(dir);
        if(dir == -1)
        {
            //Highlight first option
            currentChoice = currentCard.choice1;
        }
        else if(dir == 1)
        {
            //Highlight second option
            currentChoice = currentCard.choice2;
        }
    }

    public void SelectOption()
    {
        currentChoice.resultGameEvent.Raise(currentChoice.resourceValue);

        CloseCard();
    }
}
