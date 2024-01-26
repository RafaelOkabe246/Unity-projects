using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    //HUD and other players stuff
    public int ID;
    public ClassBasePlayerHUD[] playerHUD;
    public GameObject SelectedArrow;
    void Awake()
    {
        playerHUD = FindObjectsOfType<ClassBasePlayerHUD>();
    }

    public void fillHUD()
    {
        //playerHUD[ID].characterName.text = CharStats.CharName;
        for (int i = 0; i < playerHUD[ID].buttonsIcons.Length; i++)
        {
            playerHUD[ID].buttonsIcons[i].sprite = CharSkills[i].Icon; //filling the skills icons
            playerHUD[ID].skillCosts[i].text = (CharSkills[i].Cost).ToString(); //filling the skills costs

            for (int j = 0; j < playerHUD[ID].classBaseButtonSkills.Length; j++)
            {
                playerHUD[ID].classBaseButtonSkills[j].buttonSkill = CharSkills[j]; //filling the buttons skills
                playerHUD[ID].classBaseButtonSkills[j].playerClassBaseChar = this;
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
