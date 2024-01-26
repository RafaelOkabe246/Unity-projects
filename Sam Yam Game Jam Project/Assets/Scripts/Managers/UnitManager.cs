using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UnitManager : MonoBehaviour
{
    public Unit selectedUnit;

    public static UnitManager instance;

    public List<Unit> _playerUnits;

    public List<Unit> _enemyUnits;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        UnitsCheck();

    }

    public void SetSelectUnit(Unit unit)
    {
        selectedUnit = unit;
        ShowPathPositions(unit, GridManager.instance);
    }

    #region Spawn_units
    public void PlaceUnit(Tile spawnedTile)
    {
        //Spawn player units
        for (int i = 0; i < GridManager.instance.levelLayout.playerUnitsSpawns.Count; i++)
        {
            if ((Vector2)spawnedTile.transform.position == GridManager.instance.levelLayout.playerUnitsSpawns[i])
            {
                if (spawnedTile.occupiedUnit == null)
                {
                    Unit playerUnit = Instantiate(GridManager.instance.levelLayout.playerUnits[i], spawnedTile.transform.position, Quaternion.identity);
                    playerUnit.currentPosition = spawnedTile.transform.position;
                    playerUnit.unitFaction = Faction.PLAYER;
                    spawnedTile.occupiedUnit = playerUnit;
                    playerUnit.OccupiedTile = spawnedTile;
                    _playerUnits.Add(playerUnit);
                }
            }
        }


        //Spawn enemy units
        for (int i = 0; i < GridManager.instance.levelLayout.enemyUnitsSpawns.Count; i++)
        {
            if ((Vector2)spawnedTile.transform.position == GridManager.instance.levelLayout.enemyUnitsSpawns[i])
            {
                if (spawnedTile.occupiedUnit == null)
                {
                    Unit _enemyUnit = Instantiate(GridManager.instance.levelLayout.enemyUnits[i], spawnedTile.transform.position, Quaternion.identity);
                    _enemyUnit.currentPosition = spawnedTile.transform.position;
                    _enemyUnit.unitFaction = Faction.ENEMY;
                    spawnedTile.occupiedUnit = _enemyUnit;
                    _enemyUnit.OccupiedTile = spawnedTile;
                    _enemyUnits.Add(_enemyUnit);
                }
            }
        }
    }
    #endregion

    #region Unit_movement
    public void ShowPathPositions(Unit unit, GridManager gridManager)
    {
        int unitMaxRange = unit._unitStats.Speed;

        foreach (KeyValuePair<Vector2, Tile> keyValue in gridManager._tiles)
        {
            Tile tile = keyValue.Value;

            float DistanceUnitTile = Vector2.Distance((Vector2)unit.transform.position, (Vector2)tile.transform.position);

            if (DistanceUnitTile <= unitMaxRange && DistanceUnitTile != 0)
            {
                tile._selectionMode.SetActive(true);
                //That tile is in range
                unit.PathPositions.Add(tile);
            }
        }

    }

    public void HidePathPositions()
    {
        foreach (Tile _tile in selectedUnit.PathPositions)
        {
            _tile._selectionMode.SetActive(false);
        }
        selectedUnit.PathPositions.Clear();
    }

    public void MoveUnit(Tile tileToMove)
    {
        selectedUnit.HasActed = true;
        HidePathPositions();
        tileToMove.occupiedUnit = selectedUnit;
        selectedUnit.transform.position = tileToMove.transform.position;
        selectedUnit.currentPosition = tileToMove.transform.position;
        selectedUnit.OccupiedTile.occupiedUnit = null;
        selectedUnit.OccupiedTile = tileToMove;
        selectedUnit.HasActed = true;
        selectedUnit = null;
        GameManager.instance.ChangeState(GameState.PlayerTurn);
    }
    #endregion

    #region Units_Check

    void UnitsCheck()
    {
        //Check if all enemies or player units has acted
        if (GameManager.instance.GameState == GameState.PlayerTurn)
        {
            bool HasAllActed = _playerUnits.All(acting => acting.HasActed == true);
            if (HasAllActed)
            {
                //All player units acted, please end turn
                //Debug.Log("Encerre o turno");
            }
        }
        else if (GameManager.instance.GameState == GameState.EnemyTurn)
        {
            bool HasAllActed = _enemyUnits.All(acting => acting.HasActed == true);
            if (HasAllActed)
            {
                //All enemy units acted, start player turn
                Debug.Log("Seu turno");
            }
        }
    }

    public void ResetPlayerActions()
    {
        foreach(Unit unit in _playerUnits)
        {
            unit.HasActed = false;
        }
    }

    #endregion
}
