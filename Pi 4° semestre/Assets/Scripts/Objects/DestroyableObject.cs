using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DropableObjectType
{
    None,
    Enemy,
    Item
}

public class DestroyableObject : ObjectTile, IDamageable
{
    public ObjectStatsComponent objectStatsComponent;


    public DropableObjectType dropableObjectType;

    public GameObject dropableObject;

    [SerializeField]
    private SpriteRenderer gfx;

    [Header("Visual Feedback")]
    protected Material material;
    [ColorUsage(true, true)]
    [SerializeField] protected Color flashColor = Color.white;
    [SerializeField] protected float flashTime = 0.25f;
    [SerializeField] protected AnimationCurve flashSpeedCurve;


    void Start()
    {
        Initialize();
        currentTile.OccupyTile(this);

        material = gfx.material;
    }

    public override void Initialize()
    {

        currentTile = levelGrid.ReturnBattleTile(currentGridPosition);
        transform.position = levelGrid.ReturnBattleTile(currentGridPosition).transform.position;
    }

    void OnEnable()
    {
        objectDestroyed += OnDestroyed;
    }

    void OnDisable()
    {
        objectDestroyed -= OnDestroyed;
    }

    public void TakeDamage(int value)
    {
        if (!canTakeDamage)
            return;

        CallDamageFlash();

        TimeManager.instance.Sleep(0.1f);
        CameraController.instance.CameraShake(2f, 2f, 0.25f);
        PostProcessingManager.instance.SetTemporaryBloom(0.9f, 2f, 1f, 0.25f);

        ObjectPooler.Instance.SpawnFromPool("HitVFX", transform.position, Quaternion.identity);

        objectStatsComponent.HP -= value;
        if (objectStatsComponent.HP <= 0)
        {
            //Character dead
            DropItem();
        }
    }

    void DropItem()
    {
        switch (dropableObjectType)
        {
            case (DropableObjectType.Enemy):
                break;
            case (DropableObjectType.None):
                objectDestroyed();

                break;
            case (DropableObjectType.Item):
                GameObject newItem = Instantiate(dropableObject);
                Colectable colectable = newItem.GetComponent<Colectable>();
                colectable.levelGrid = levelGrid;
                colectable.currentGridPosition = currentGridPosition;
                colectable.Initialize();
                objectDestroyed();

                //upgradeColectable.currentTile = levelGrid.ReturnBattleTile(upgradeColectable.currentGridPosition);
                //upgradeColectable.currentTile.OccupyTile(gameObject);

                break;
        }
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

    protected override void OnDestroyed()
    {
        base.OnDestroyed();
        SoundManager.instance.PlayAudio(AudiosReference.objectDestroyed , AudioType.OBJECT, null);
        ProgressionManager.instance.currentRunInfo.objectsDestroyed++;
    }

    public void ResistDamage()
    {
        throw new System.NotImplementedException();
    }
}
