using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCrystal : MonoBehaviour, ICollectableItem
{
    [HideInInspector] public CrystalsManager crystalsManager;
    public Animator anim;
    public Transform gfxTransform;
    private Collider2D col;
    private AudioSource audioSource;

    private Vector3 initialPosition;

    public Transform spiritTrans;

    [Header("When collected")]
    private bool collected;
    [SerializeField] private float speedWhenCollected = 20f;
    private bool canCollect = true;

    private void Awake()
    {
        initialPosition = transform.position;
    }

    private void OnEnable()
    {
        transform.position = initialPosition;
        col = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
        col.enabled = true;
        collected = false;
        canCollect = true;
        anim.Play("ANIM_Crystal");

        PlayerActions.OnTriggerDeath += OnTriggerPlayerDeath;
    }

    private void OnDisable()
    {
        if (collected && canCollect)
        {
            crystalsManager.CollectCoin(1);
            crystalsManager.crystalsUI.CallCollectAnimationInUI();
            collected = false;
        }

        PlayerActions.OnTriggerDeath -= OnTriggerPlayerDeath;
    }

    private void Update()
    {
        if (collected)
        {

            Vector3 UIworldPoint = crystalsManager.crystalsUI.GetCrystalsUIworldPosition();
            float distance = Vector3.Distance(UIworldPoint, gfxTransform.position);

            if (distance > 0.01f)
            {
                float step = speedWhenCollected * Time.deltaTime;
                Vector3 targetPosition = Vector3.MoveTowards(transform.position, UIworldPoint, step);
                transform.position = targetPosition;
            }
            else
            {
                crystalsManager.CollectCoin(1);
                crystalsManager.crystalsUI.CallCollectAnimationInUI();
                collected = false;
                gameObject.SetActive(false);
            }
        }
    }

    public void CollectItem()
    {
        SoundsManager.instance.PlayAudio(AudiosReference.crystalCollect, AudioType.COLLECTABLE, audioSource);
        anim.SetTrigger("Collect");
        StageBlocksHandler.collectedCrystalsInBlock.Add(this);
        crystalsManager.crystalsUI.CallShowAnimationInUI();
        col.enabled = false;
        OnCollected();
        //spiritTrans.gameObject.SetActive(true);
        //spiritTrans.SetParent(null);
    }

    public void OnCollected()
    {
        collected = true;
    }

    private void OnTriggerPlayerDeath()
    {
        canCollect = false;
    }
}
