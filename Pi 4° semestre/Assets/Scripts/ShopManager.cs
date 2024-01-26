using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    public Colectable currentColectable;
    [SerializeField] private Upgrade currentUpgrade;

    private void Awake()
    {
        instance = this;
    }

    public void ResgisterColectable(Component _colectable, object _data)
    {
        if(_colectable is Colectable)
        {
            currentColectable = (Colectable)_colectable;
            currentUpgrade = currentColectable.upgrade;
        }

    }

    public void TryToBuy()
    {
        
        if (CoinsManager.instance.currentCoins - currentUpgrade.cost >= 0)
        {
            currentColectable.OnCollected();
            ScreenStack.instance.RemoveScreenFromStack(GameplayUIsContainer.instance.GetShopScreen());
            SoundManager.instance.PlayAudio(AudiosReference.buyItem, AudioType.COLLECTABLE, null);
        }
        else
        {
            SoundManager.instance.PlayAudio(AudiosReference.failBuyItem, AudioType.COLLECTABLE, null);
            return;
        }
        
    }
}
