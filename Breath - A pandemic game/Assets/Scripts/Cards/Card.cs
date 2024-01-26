using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Choice
{
    public int resourceValue;
    public string choiceText;
    public GameEvent resultGameEvent;
}

[CreateAssetMenu(menuName = "Card")]
public class Card : ScriptableObject
{
    public Characters character;
    public string mainText;
    public string characterName;

    public Choice choice1;
    public Choice choice2;


    private void OnValidate()
    {
        switch (character)
        {
            case Characters.Player:
                break;
            case Characters.Professor:
                break;
            case Characters.Nurse_1:
                break;
            case Characters.Nurse_2:
                break;
            case Characters.Recepcionist:
                break;
            case Characters.Surgeon:
                break;
            case Characters.Janitor:
                break;
            case Characters.Paramedic:
                break;
            case Characters.Security:
                break;
            case Characters.AmbulanceDriver:
                break;
            case Characters.Negationist:
                break;
            case Characters.Grandma:
                break;
        }
    }
}