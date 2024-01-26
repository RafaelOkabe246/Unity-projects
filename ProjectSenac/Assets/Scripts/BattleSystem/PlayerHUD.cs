using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public int ID;

    [Header ("UI")]
    public Text characterName;
    public Image[] buttonsIcons;
    public SkillButton[] buttonsSkills; //player will choose a button and this button will return a number(button ID), which will determine the selected skill
    public Text[] skillCosts;
}
