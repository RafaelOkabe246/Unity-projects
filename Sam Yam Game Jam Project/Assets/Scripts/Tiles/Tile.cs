using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Tile : MonoBehaviour
{


    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    public SpriteRenderer _terrainSprite;

    public GameObject _highLight;
    public GameObject _selectionMode;

    public Unit occupiedUnit;
    public TerrainType _terrainType;

    public virtual void InializatingTile(int x, int y)
    {
        var isOffset = (x + y) % 2 == 1;

        _spriteRenderer.color = isOffset ? _offsetColor : _baseColor;

        if(_terrainType != null)
        {
            _terrainSprite.sprite = _terrainType._terrainSprite;
        }
        else
        {
            _terrainSprite.color = new Color(_terrainSprite.color.r, _terrainSprite.color.g, _terrainSprite.color.b, 0);
        }
    }

    private void Update()
    {
        if(GameManager.instance.GameState != GameState.PlayerTurn)
        {
            _selectionMode.SetActive(false);
        }
    }

    private void OnMouseEnter()
    {
        _highLight.SetActive(true);
    }

    private void OnMouseExit()
    {
        _highLight.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (GameManager.instance.GameState == GameState.PlayerTurn)
        {
            GridManager.instance.selectedTile = this;
            UiManager.instance.SetTerrainImage(this);
            if (occupiedUnit != null)
            {
                if (occupiedUnit.unitFaction == Faction.PLAYER)
                {
                    if (occupiedUnit.HasActed == false)
                    {
                        //Selected ally unit
                        UnitManager.instance.SetSelectUnit(occupiedUnit);
                        UiManager.instance.SetUnitInfo(occupiedUnit);
                        GameManager.instance.ChangeState(GameState.UnitSelected);
                    }
                    else
                    {
                        //Unit already acted
                        return;
                    }
                }
                else if (occupiedUnit.unitFaction == Faction.ENEMY)
                {
                    //see enemy unity stats

                }
            }
            else if (occupiedUnit == null)
            {
                //see the terrain
                return;
            }
        }
        else if (GameManager.instance.GameState == GameState.UnitSelected)
        {
            //One unit is selected, get the path positions if one of the path positions is equal to the tile position, move the player
            if (occupiedUnit != null)
            {
                if (occupiedUnit.unitFaction == Faction.PLAYER)
                {
                    //The tile is occupied
                    return;
                }
                else if (occupiedUnit.unitFaction == Faction.ENEMY)
                {

                    //Attack the enemy 

                    //Hide the path positions
                    UnitManager.instance.HidePathPositions();
                    BattleManager.instance.StartBattle(UnitManager.instance.selectedUnit, occupiedUnit, _terrainType);
                    
                }
            }
            else if (occupiedUnit == null)
            {
                //Move
                try
                {
                    for (int i = 0; i < UnitManager.instance.selectedUnit.PathPositions.Count; i++)
                    {
                        if (UnitManager.instance.selectedUnit.PathPositions[i] == this)
                        { 
                            UnitManager.instance.MoveUnit(this);
                        }
                    }
                }
                catch (Exception)
                {
                    return;
                }
            }


        }
    }

}
