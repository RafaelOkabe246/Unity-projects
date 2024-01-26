using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public Skills buttonSkill;
    public PlayerUnit playerChar; //player character - it's useful when the player wants to target itself

    public Unit skillTargetUnit; //skill target

    public Text SkillName; //Shows when the skill button is pressed

    private BattleSystem bs;

    public void SelectSkill() {
        bs = FindObjectOfType<BattleSystem>();
        if (buttonSkill.Cost <= bs.actOrbs)
        {
            if (buttonSkill.skillTargetType == 0 || buttonSkill.skillTargetType == 3 || buttonSkill.skillTargetType == 4)
            {
                SkillOnSelf();
            }
            else {
                StartSkillSelection();
            }
        }
        else {
            //Player can't select this skill, because it needs more action points;
            Debug.Log("Não há pontos de ação suficientes para usar essa habilidade!");
        }
    }

    #region Skill On Self
    void SkillOnSelf() {
        bs.actOrbs -= buttonSkill.Cost;
        bs.addSkill(buttonSkill, buttonSkill.skillTargetType, playerChar); //adding the selected skill and the skill target to the list
        bs.onPlayerSelectAction(); //start the next character skill selection
    }
    #endregion

    #region Skill On Target
    void StartSkillSelection() {
        Debug.Log("SELECT A TARGET!");
        ShowSkillDescription();
        bs.mouseClick.enabled = true;
        bs.selectedButton = this;
        bs.state = BattleState.PLAYER_TARGET_SELECT;
    }

    public void SkillOnTarget(){
        if ((buttonSkill.skillTargetType == 1 && skillTargetUnit.GetComponent<PlayerUnit>() != null) ||
            (buttonSkill.skillTargetType == 2 && skillTargetUnit.GetComponent<EnemyUnit>() != null)) //Checking if the target is the correct target type
        {
            Debug.Log("CORRECT TARGET SELECTED!");
            bs.actOrbs -= buttonSkill.Cost;
            bs.addSkill(buttonSkill, buttonSkill.skillTargetType, skillTargetUnit); //Attack the selected unit
            bs.mouseClick.enabled = false;
            bs.onPlayerSelectAction(); //start the next character skill selection
        }
        else {
            Debug.Log("Select a compatible target!");
            StartSkillSelection();
        }
    }
    #endregion

    public void ShowSkillDescription() {
        //Shows the skill description when the player selects the button
        SkillName.text = buttonSkill.Name;
    }

}
