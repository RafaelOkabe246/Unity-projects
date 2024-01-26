using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCoin : MonoBehaviour, ICollectableItem
{
    [HideInInspector] public CoinsManager coinsManager;
    private Animator anim;
    public Transform gfxTransform;
    private AudioSource audioSource;

    private Vector3 initialPosition;

    [Header("When collected")]
    private bool collected;
    [SerializeField] private float speedWhenCollected = 20f;

    private void Awake()
    {
        initialPosition = transform.position;
    }

    private void OnEnable()
    {
        transform.position = initialPosition;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnDisable()
    {
        if (collected)
        {
            coinsManager.CollectCoin(1);
            coinsManager.fruitsUI.CallCollectAnimationInUI();
            collected = false;
        }
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (collected) 
        {
            Vector3 UIworldPoint = coinsManager.fruitsUI.GetFruitsUIworldPosition();
            float distance = Vector3.Distance(UIworldPoint, gfxTransform.position);

            if (distance > 0.01f) 
            {
                float step = speedWhenCollected * Time.deltaTime;
                Vector3 targetPosition = Vector3.MoveTowards(transform.position, UIworldPoint, step);
                transform.position = targetPosition;
            }
            else
            {
                coinsManager.CollectCoin(1);
                coinsManager.fruitsUI.CallCollectAnimationInUI();
                collected = false;
                gameObject.SetActive(false);
            }
        }
    }

    public void CollectItem() 
    {
        SoundsManager.instance.PlayAudio(AudiosReference.fruitCollect, AudioType.COLLECTABLE, audioSource);
        anim.SetTrigger("Collect");
        StageBlocksHandler.collectedFruitsInBlock.Add(this);
    }

    public void OnCollected()
    {
        collected = true;
    }

}
