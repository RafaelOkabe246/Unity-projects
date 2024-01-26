using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CharacterCollision : MonoBehaviour, IDamageable
{
    public CharacterActions characterActions;
    [SerializeField] protected Character character;
    [SerializeField] protected Collider2D coll;
    public bool canTakeDamage;
    public bool canIgnoreTakeDamage;

    [Header("Visual Feedback")]
    protected Material material;
    [ColorUsage(true, true)]
    [SerializeField] protected Color flashColor = Color.white;
    [SerializeField] protected float flashTime = 0.25f;
    [SerializeField] protected AnimationCurve flashSpeedCurve;

    protected virtual void Start()
    {
        coll = GetComponent<Collider2D>();
        character = GetComponent<Character>();
        material = characterActions.OnGetSpriteGFX().material;
    }

    void OnEnable()
    {
        characterActions.OnCanTakeDamage += CheckCanTakeDamage;
        characterActions.SetCanTakeDamage += SetCanTakeDamage;
        characterActions.OnCanIgnoreTakeDamageAnim += IgnoreTakeDamage;
    }

    void OnDisable()
    {
        characterActions.OnCanTakeDamage -= CheckCanTakeDamage;
        characterActions.SetCanTakeDamage -= SetCanTakeDamage;
        characterActions.OnCanIgnoreTakeDamageAnim -= IgnoreTakeDamage;
    }

    #region FUNC_METHODS
    bool IgnoreTakeDamage()
    {
        return canIgnoreTakeDamage;
    }

    bool CheckCanTakeDamage()
    {
        return canTakeDamage;
    }
    #endregion

    void SetCanTakeDamage(bool i)
    {
        canTakeDamage = i;
    }

    public void TakeDamage(int value)
    {
        //Check upgrades
        //value = TriggerEditDamageEffects(value);
        if (!characterActions.OnCanTakeDamage())
            return;

        CallDamageFlash();

        TimeManager.instance.Sleep(0.1f);
        CameraController.instance.CameraShake(2f,2f, 0.25f);
        PostProcessingManager.instance.SetTemporaryBloom(0.9f, 2f, 1f, 0.25f);

        ObjectPooler.Instance.SpawnFromPool("HitVFX", transform.position, Quaternion.identity);

        character.statsComponent.HP -= value;
        if(character.statsComponent.HP <= 0)
        {
            //Character dead
            //If is moving, delay dead until it reaches the tile
            if(character is Enemy)
            {
                characterActions.OnPlayAudio(AudiosReference.robotDestroyed, false);
                Debug.Log("GFEG");
            }
            StartCoroutine(nameof(StartDeath));
        }
        else
        {
            characterActions.OnPlayAudio(AudiosReference.lightAttack, false);

//            if (character is Player)
  //              characterActions.OnPlayAudio(AudiosReference.playerTakeDamage, true);
        }

        if (!characterActions.OnCanIgnoreTakeDamageAnim())
            characterActions.OnTakeDamage();
    }

    private IEnumerator StartDeath()
    {
        yield return new WaitUntil(() => !characterActions.OnCheckIsMoving());
        characterActions.OnTriggerDeath();
    }

    private int TriggerEditDamageEffects(int damageValue)
    {
        foreach (Upgrade _upgrade in characterActions.GetUpgrades())
        { 
            if(_upgrade.upgradeType == UpgradeType.EditDamage)
            {
                //TO DO:
                //Create referece for attacking character for owner
                damageValue += _upgrade.TriggerEffect(character, character, damageValue);
            }
        }

        return damageValue;
    }

    protected void SetFlashColor(Material materialToSet)
    {
        materialToSet.SetColor("_FlashColor", flashColor);
    }

    protected void SetFlashAmount(float amount, Material materialToSet)
    {
        materialToSet.SetFloat("_FlashAmount", amount);
    }

    protected void CallDamageFlash()
    {
        StartCoroutine(DamageFlasher());
    }

    protected virtual IEnumerator DamageFlasher()
    {
        SetFlashColor(material);

        float currentFlashAmount = 0f;
        float elapsedTime = 0f;
        while (elapsedTime < flashTime)
        {
            elapsedTime += Time.deltaTime;

            currentFlashAmount = Mathf.Lerp(1f, flashSpeedCurve.Evaluate(elapsedTime), (elapsedTime / flashTime));
            SetFlashAmount(currentFlashAmount, material);

            yield return null;
        }
    }

    public void ResistDamage()
    {
        throw new System.NotImplementedException();
    }
}
