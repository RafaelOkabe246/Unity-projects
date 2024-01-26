using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class UpgradeCollectable : ObjectTile, IInteractable
{
    public Upgrade upgrade;
    [SerializeField] 
    private Animator anim;

    private Character playerReference;

    public bool needCoins;

    [SerializeField]
    private float delayToDeactivate = 0.25f;
    
    [Header("Show info")]
    [SerializeField]
    private GameObject shopInfo;
    [SerializeField] 
    private TextMeshPro costText;
    [SerializeField] 
    private SpriteRenderer upgradeIcon;

    void Start()
    {
        shopInfo.SetActive(false);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //playerReference = collision.gameObject.GetComponent<Character>();
            //ShowShopInfo();
        }
    }
    public void InteractionEvent(object _data)
    {
        throw new System.NotImplementedException();
    }

    public void InteractionEvent()
    {
        TryToBuy();
    }

    void TryToBuy()
    {
        if (CoinsManager.instance.currentCoins - upgrade.cost >= 0)
        {
            OnCollected();
        }
        else
        {
            Debug.Log("Not enought money");
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
            OnCollected();
        }
    }

    private void OnCollected()
    {
        anim.SetTrigger("Collect");
        CharacterStatsComponent playerStatsComponent = playerReference.statsComponent;
        playerStatsComponent.ApplyUpgrade(upgrade);
        CoinsManager.instance.SpendCoins(upgrade.cost);

        StartCoroutine(DeactivateCollectable());
    }

    private IEnumerator DeactivateCollectable() 
    {
        yield return new WaitForSeconds(delayToDeactivate);

        objectDestroyed();
    }

}
