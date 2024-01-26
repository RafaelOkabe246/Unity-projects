using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum statusEffect {NEUTRAL, POISON, REGEN} //NEUTRAL is used when the unit has any status effect
public class Unit : MonoBehaviour
{

    public UnitStats unitStat; //Sets all the unit's stats

    public SpriteRenderer spriteRenderer;
    public Animator anim;

    [Header("Health")]

    public float HP;

    [Header("HUD")]

    public GameObject hpBarPanel;
    public Slider hpBar;
    public Text LifePoints;

    public bool isDead;

    public statusEffect stef;
    public int statEffectDuration; //how many turns the effect will last

    void Start()
    {
        HP = unitStat.UnitMaxHP;
        SetMaxHealth(unitStat.UnitMaxHP);
        SetHealth();

        anim = GetComponent<Animator>();
        
    }

    void FixedUpdate()
    {
        //check if the unit is dead
        if (HP <= 0)
        {
            HP = 0;
            isDead = true;
            //Visual feedback of the unit death
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, .3f);
        }
        else {
            isDead = false;
        }
    }

    #region Control HUD
    public void SetMaxHealth(float maxHealth)
    {
        hpBar.maxValue = maxHealth;
        hpBar.value = unitStat.UnitMaxHP;
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

    #region StatsEffects
    //Functions that will change the character's stats
    public void TakeDamage(float DamageTaken)
    {
        SetHealth();
        HP -= DamageTaken;
        if (HP <= 0)
        {
            HP = 0;
        }
    }

    public void Healing(float HealthPoints)
    {
        SetHealth();
        HP += HealthPoints;
        if (HP >= unitStat.UnitMaxHP)
        {
            HP = unitStat.UnitMaxHP;
        }
    }

    public void CheckStatusEffect() {
        switch (stef) {
            case statusEffect.POISON:
                HP -= 1;
                Debug.Log(unitStat.UnitName + " took damage from poison");
                break;
            case statusEffect.REGEN:
                HP += 1;
                Debug.Log(unitStat.UnitName + " regenarated it's HP");
                break;
        }
        if (stef != statusEffect.NEUTRAL)
        {
            statEffectDuration--;
            //stopping the status effect
            if (statEffectDuration <= 0)
            {
                statEffectDuration = 0;
                stef = statusEffect.NEUTRAL;
                Debug.Log("The effect vanished!");
            }
        }
    }

    #endregion
}
