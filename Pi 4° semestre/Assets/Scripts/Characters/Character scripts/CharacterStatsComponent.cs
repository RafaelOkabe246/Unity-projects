using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
using System;
using TMPro;

public class CharacterStatsComponent : StatsComponent
{
    public Character character;

    [Header("Combat")]
    public int lightHitDamage;
    [SerializeField] private int hitAccumulation;
    public int maxHitAccumulation;
    public float knockedTime;
    public float resetKnockedTime;
    
    bool HasTriggerKnockdownTimer;
    private bool isKnockdown;

    [Header("Movement")]
    public float moveSpeed;

    private void OnEnable()
    {
        character.characterActions.OnCheckKnockdown += CheckKnockdown;
        character.characterActions.GetMoveSpeed += OnGetMoveSpeed;
        character.characterActions.OnTakeDamage += OnHitAcumulation;
        character.characterActions.KnockdownTime += OnCheckKnockedTime;
        character.characterActions.GetLifeText += GetLifeText;
    }

    private void OnDisable()
    {
        character.characterActions.OnCheckKnockdown -= CheckKnockdown;
        character.characterActions.GetMoveSpeed -= OnGetMoveSpeed;
        character.characterActions.OnTakeDamage -= OnHitAcumulation;
        character.characterActions.KnockdownTime -= OnCheckKnockedTime;
        character.characterActions.GetLifeText -= GetLifeText;
    }

    #region FUNC_Methods
    CharacterStatsComponent GetCharacterStatsComponent()
    {
        return this;
    }
    private bool CheckKnockdown()
    {
        return isKnockdown;
    }
    private float OnCheckKnockedTime()
    {
        return knockedTime;
    }

    private float OnGetMoveSpeed()
    {
        return moveSpeed;
    }

    private TextMeshPro GetLifeText() 
    {
        return lifeText;
    }
    #endregion

    #region ACTION_Methods
    void OnHitAcumulation()
    {
        hitAccumulation++;
        if(hitAccumulation >= maxHitAccumulation)
        {
            KnockdownResetTimer();

            //Knockdown character
            character.characterActions.OnKnockDown(true);
            isKnockdown = true;
            hitAccumulation = 0;
            character.characterActions.OnChangeCharacterState(CharacterStates.Knocked);
            SoundManager.instance.PlayAudio(AudiosReference.robotTakeKnockdown, AudioType.CHARACTER, null);
            StartCoroutine(nameof(KnockdownTimer));
        }
        else 
        {
            if(!HasTriggerKnockdownTimer)
                KnockdownResetTimer();
        }
    }
    #endregion

    //Knockdown
    IEnumerator KnockdownTimer()
    {
        character.characterActions.OnGetSpriteGFX().color = Color.black;
        yield return new WaitForSeconds(knockedTime);
        character.characterActions.OnKnockDown(false);
        yield return new WaitForSeconds(character.characterActions.OnGetCharacterAnimations().anim.GetCurrentAnimatorStateInfo(0).length);
        isKnockdown = false;
        character.characterActions.OnChangeCharacterState(CharacterStates.Alive);
    }

    async void KnockdownResetTimer()
    {
        float timer = 0;
        timer += Time.deltaTime;
        await Task.Delay((int)resetKnockedTime * 1000);
        HasTriggerKnockdownTimer = false;
        hitAccumulation = 0;
    }

    
    public void ApplyUpgrade(Upgrade upgrade)
    {
        if (currentUpgrades.ContainsKey(upgrade) && !upgrade.doesStack)
            return;

        if (currentUpgrades.ContainsKey(upgrade))
        {
            currentUpgrades[upgrade]++;
            return;
        }

        currentUpgrades.Add(upgrade, 1);
        TriggerUpdate(upgrade);
    }

    public void RemoveUpgrade(Upgrade upgrade)
    {
        if (!currentUpgrades.ContainsKey(upgrade))
            return;

        if(currentUpgrades.ContainsKey(upgrade) && upgrade.doesStack)
        {
            currentUpgrades[upgrade]++;
            if (currentUpgrades[upgrade] <= 0)
                currentUpgrades.Remove(upgrade);
            return;
        }

        currentUpgrades.Remove(upgrade);
    }

    void TriggerUpdate(Upgrade upgrade)
    {
        switch (upgrade.upgradeType)
        {
            case (UpgradeType.OnApplication): //Add a new mechanic
                upgrade.TriggerEffect(character);
                break;
            case (UpgradeType.Consume): //Its is instant called
                upgrade.TriggerEffect(character);
                RemoveUpgrade(upgrade);
                break;
        }
    }


    public bool TryTriggerUpgradesOfSpecificType(UpgradeType _upgradeType)
    {
        bool result = false;

        foreach (KeyValuePair<Upgrade, int> upgrade in currentUpgrades)
        {
            for (int i = 0; i < upgrade.Value; i++)
            {
                if(upgrade.Key.upgradeType == _upgradeType)
                {
                    result = true;
                    TriggerUpdate(upgrade.Key);
                }
            }
        }
        return result;
    }
    
}
