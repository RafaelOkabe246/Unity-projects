using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUnit : Unit
{
    public int ID;
    public PlayerHUD[] playerHUD;
    public GameObject SelectedArrow;
    void Awake()
    {
        playerHUD = FindObjectsOfType<PlayerHUD>();
    }

    public void fillHUD() {
        playerHUD[ID].characterName.text = unitStat.UnitName;
        for (int i = 0; i < playerHUD[ID].buttonsIcons.Length; i++)
        {
            playerHUD[ID].buttonsIcons[i].sprite = unitStat.characterSkills[i].Icon; //filling the skills icons
            playerHUD[ID].skillCosts[i].text = (unitStat.characterSkills[i].Cost).ToString(); //filling the skills costs
            
            for (int j = 0; j < playerHUD[ID].buttonsSkills.Length; j++) {
                playerHUD[ID].buttonsSkills[j].buttonSkill = unitStat.characterSkills[j]; //filling the buttons skills
                playerHUD[ID].buttonsSkills[j].playerChar = this;
            }
        }
        Debug.Log("HUD CHANGED!!!");
    }

    public void EnablePlayerHUD()
    {
        playerHUD[ID].gameObject.SetActive(true);
        SelectedArrow.SetActive(true);
    }

    public void DisablePlayerHUD()
    {
        playerHUD[ID].gameObject.SetActive(false);
        SelectedArrow.SetActive(false);
    }
}
