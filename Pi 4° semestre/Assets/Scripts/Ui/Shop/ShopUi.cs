using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopUi : ActivatableUI
{
    [Header("Colectable info")]
    public TextMeshProUGUI upgradeName;
    public TextMeshProUGUI upgradeCost;
    public TextMeshProUGUI upgradeDescription;
    public Image upgradeIcon;

    public void SetShopInfo()
    {
        Colectable _colectable = ShopManager.instance.currentColectable;

        upgradeName.text = _colectable.upgrade.upgradeName;
        upgradeCost.text = "R$ " + _colectable.upgrade.cost;
        upgradeDescription.text = _colectable.upgrade.upgradeDescription;
        upgradeIcon.sprite = _colectable.upgrade.icon;

    }
}
