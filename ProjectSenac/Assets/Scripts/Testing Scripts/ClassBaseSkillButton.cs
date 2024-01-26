using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassBaseSkillButton : MonoBehaviour
{
    public Skills buttonSkill;
    public Player playerClassBaseChar; //player character - it's useful when the player wants to target itself

    public Character skillTargetCharacter; //skill target

    public Text SkillName; //Shows when the skill button is pressed

    private ClassBaseBattleSystem cbs;


    public void SelectSkill()
    {
        cbs = FindObjectOfType<ClassBaseBattleSystem>();
        
        if (buttonSkill.skillTargetType == 0 || buttonSkill.skillTargetType == 3 || buttonSkill.skillTargetType == 4)
        {
            SkillOnSelf();
        }
        else
        {
            StartSkillSelection();
        }
        
    }

    #region Skill On Self
    void SkillOnSelf()
    {
        //cbs.actOrbs -= buttonSkill.Cost;
        //cbs.addSkill(buttonSkill, buttonSkill.skillTargetType, playerChar); //adding the selected skill and the skill target to the list
        cbs.onPlayerSelectAction(); //start the next character skill selection

        //Changing the variables of the characterAction
        playerClassBaseChar.characterAction.Emissor = playerClassBaseChar;
        playerClassBaseChar.characterAction.Target = skillTargetCharacter; //Single target
        cbs.PlayerActions.Add(playerClassBaseChar.characterAction); //adding the action to the list
    }
    #endregion

    #region Skill On Target
    void StartSkillSelection()
    {
        Debug.Log("SELECT A TARGET!");
        ShowSkillDescription();
        cbs.mouseClick.enabled = true;
        cbs.selectedButton = this;
        cbs.state = ClassBaseBattleState.PLAYER_TARGET_SELECT;
        
    }

    public void SkillOnTarget()
    {
        
        if ((buttonSkill.skillTargetType == 1 && skillTargetCharacter.GetComponent<Player>() != null) ||
            (buttonSkill.skillTargetType == 2 && skillTargetCharacter.GetComponent<Enemy>() != null)) //Checking if the target is the correct target type
        {
            Debug.Log("CORRECT TARGET SELECTED!");
            //cbs.actOrbs -= buttonSkill.Cost;
            //cbs.addSkill(buttonSkill, buttonSkill.skillTargetType, skillTargetUnit); //Attack the selected unit

            //Changing the variables of the characterAction
            playerClassBaseChar.characterAction.Emissor = playerClassBaseChar;
            playerClassBaseChar.characterAction.Target = skillTargetCharacter; //Single target
            cbs.PlayerActions.Add(playerClassBaseChar.characterAction); //adding the action to the list

            cbs.mouseClick.enabled = false;
            cbs.onPlayerSelectAction(); //start the next character skill selection
        }
        else
        {
            Debug.Log("Select a compatible target!");
            StartSkillSelection();
        }
    }
    #endregion

    public void ShowSkillDescription()
    {
        //Shows the skill description when the player selects the button
        SkillName.text = buttonSkill.Name;
    }
}
