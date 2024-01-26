using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class Colectable : TriggerEventObject
{
    public Upgrade upgrade;
    [SerializeField] private Animator anim;

    private Character playerReference;

    public bool needCoins;

    [Header("Show info")]
    [SerializeField]
    private GameObject shopInfo;
    [SerializeField] private TextMeshPro costText;
    [SerializeField] private SpriteRenderer upgradeIcon;

    void Start()
    {
       // shopInfo.SetActive(false);
    }

    void OnEnable()
    {
        objectDestroyed += OnDestroyed;
    }

    void OnDisable()
    {
        objectDestroyed -= OnDestroyed;
    }


    public override void SetCountdownInteractableEvent(bool _hasPlayer)
    {
        if (_hasPlayer)
        {
            playerReference = currentTile.GetOccupyingObject().GetComponent<Player>(); //collision.gameObject.GetComponent<Character>();
            StopCoroutine(nameof(CountdownInteractableEvent));
            StartCoroutine(nameof(CountdownInteractableEvent));
        }
        else
        {
            StopCoroutine(nameof(CountdownInteractableEvent));
        }
    }

    public override void InteractionEvent()
    {
        StartCoroutine(nameof(CountdownInteractableEvent));
    }

    protected override IEnumerator CountdownInteractableEvent()
    {
        yield return new WaitForSeconds(triggerTime);
        CheckNeedMoney();
    }

    void CheckNeedMoney()
    {
        if (!needCoins)
        {
            OnCollected(); //Collect direct without shop
        }
        else
        {
            TriggerEvent.Raise(this, upgrade.cost);
        }
    }

    public void TryToBuy()
    {
        if (CoinsManager.instance.currentCoins - upgrade.cost >= 0)
        {
            OnCollected();
        }
        else
        {
            //Show some message
            Debug.Log("Not enought money");
            return;
        }
    }

    void ShowShopInfo()
    {
        if (needCoins)
        {
            shopInfo.SetActive(true);
            upgradeIcon.sprite = upgrade.icon;
            costText.text = "Cost: " + upgrade.cost;
        }
        else
        {
            shopInfo.SetActive(false);
            //OnCollected();
        }
    }

    public void OnCollected()
    {
        CharacterStatsComponent playerStatsComponent = playerReference.statsComponent;
        playerStatsComponent.ApplyUpgrade(upgrade);
        CoinsManager.instance.SpendCoins(upgrade.cost);
        objectDestroyed();
    }

}
