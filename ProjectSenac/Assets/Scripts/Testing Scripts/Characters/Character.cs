using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{

    public ClassBaseBattleSystem CBS;
    public SpriteRenderer spriteRenderer;
    public Animator anim;

    [Header("Stats")]
    public bool IsDead;
    public float HP;
    public CharacterStatsScriptableObject CharStats;

    //Personal abilities
    [Header("Skills")]
    public List<Skills> CharSkills;
    public Action characterAction;

    [Header("HUD")]

    public GameObject hpBarPanel;
    public Slider hpBar;
    public Text LifePoints;

    void Start()
    {
        HP = CharStats.MaxLife;
        CBS = FindObjectOfType<ClassBaseBattleSystem>();

        anim = GetComponent<Animator>();
    }

    public void LifeAffect(float points)
    {
        HP += points;
    }

    public void DefineAction()
    {

    }


    #region Control HUD
    public void SetMaxHealth(float maxHealth)
    {
        hpBar.maxValue = maxHealth;
        hpBar.value = CharStats.MaxLife;
    }

    public void SetHealth()
    {
        hpBar.value = HP;
        LifePoints.text = HP.ToString();
    }
    #endregion

    #region Enable/Disable HUD

    public void EnableHPBar()
    {
        hpBarPanel.SetActive(true);
    }

    public void DisableHPBar()
    {
        hpBarPanel.SetActive(false);
    }
    #endregion
}
