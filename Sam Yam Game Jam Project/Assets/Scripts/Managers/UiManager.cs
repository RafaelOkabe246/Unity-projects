using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    [Header("Terrain and troops")]
    public Image troopImage;
    public TextMeshProUGUI troopText;

    public Image terrainImage;
    public TextMeshProUGUI terrainText;

    [Header("Turns, battles and overall chance of winning")]
    public TextMeshProUGUI turns;

    public GameObject endTurnButton;

    public Image battleBalanceImage;
    public TextMeshProUGUI battleStateText;


    [Header("Messages")]
    public GameObject TextBox;

    private void Awake()
    {
        instance = this;
    }

    #region Terrain
    public void SetTerrainImage(Tile terrain)
    {
        terrainImage.sprite = terrain._terrainSprite.sprite;
    }

    public void SetTerrainText()
    {

    }
    #endregion

    #region Troops
    public void SetUnitInfo(Unit unit)
    {
        troopImage.sprite = unit._sprite.sprite;
    }


    #endregion
}
