using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Contains the informations of the level grid.
/// </summary>
public class Grid : MonoBehaviour
{
    [Header("Grid tiles")]
    public int gridWidth, gridHeight;
    public Dictionary<Vector2, BattleTile> battleTiles = new Dictionary<Vector2, BattleTile>(); //Nodes or waypoints
    public BattleTile battleTilePrefab;
    public GameObject spawnerParentTile;
    public float xTileOffSet, yTileOffSet;

    public IEnumerator Generate()
    {
        GameObject[] tilesArray = new GameObject[gridWidth * gridHeight];
        int currentTilesIndex = -1;
        int tileColor = 0;
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {

                currentTilesIndex++;

                float offSetX = (xTileOffSet * x);
                float offSetY = (yTileOffSet * y);

                BattleTile spawnedTile = Instantiate(battleTilePrefab, new Vector3(x + offSetX, y + offSetY), Quaternion.identity, spawnerParentTile.transform);
                spawnedTile.name = $"Tile: {x} {y}";
                spawnedTile.levelGrid = this;
                spawnedTile.gridPosition = new Vector2(x, y);
                battleTiles[new Vector2(x, y)] = spawnedTile;
                tilesArray[currentTilesIndex] = spawnedTile.gameObject;
                spawnedTile.SetSortingLayerBase(-y);

                spawnedTile.ChooseColor(tileColor);
                tileColor = (tileColor == 0) ? 1 : 0;

                yield return new WaitForSeconds(0.0075f);
            }
        }

        foreach (BattleTile _battleTile in battleTiles.Values)
        {
            SetNeighboursTiles(_battleTile);
            SetRetreatTiles(_battleTile);
        }
        Camera.main.transform.position = new Vector3((float)gridWidth / 2 - 0.5f, (float)gridHeight / 2 - 0.5f, -10);

    }

    private void SetRetreatTiles(BattleTile _battleTile)
    {
        float xPos = _battleTile.gridPosition.x;
        float yPos = _battleTile.gridPosition.y;

        //Down
        if (yPos - 2 >= 0)
        {
            _battleTile.retreatTiles.Add(ReturnBattleTile(new Vector2(xPos, yPos - 2)));
            if (xPos + 1 < gridWidth)
                _battleTile.retreatTiles.Add(ReturnBattleTile(new Vector2(xPos + 1, yPos - 2)));
            if (xPos - 1 >= 0)
                _battleTile.retreatTiles.Add(ReturnBattleTile(new Vector2(xPos - 1, yPos - 2)));
        }

        //Up
        if (yPos + 2 < gridHeight)
        {
            _battleTile.retreatTiles.Add(ReturnBattleTile(new Vector2(xPos, yPos + 2)));
            if (xPos + 1 < gridWidth)
                _battleTile.retreatTiles.Add(ReturnBattleTile(new Vector2(xPos + 1, yPos + 2)));
            if (xPos - 1 >= 0)
                _battleTile.retreatTiles.Add(ReturnBattleTile(new Vector2(xPos - 1, yPos + 2)));
        }

        //Left
        if (xPos - 2 >= 0)
        {
            _battleTile.retreatTiles.Add(ReturnBattleTile(new Vector2(xPos - 2, yPos)));
            if (yPos + 1 < gridHeight)
                _battleTile.retreatTiles.Add(ReturnBattleTile(new Vector2(xPos - 2, yPos + 1)));
            if (yPos - 1 >= 0)
                _battleTile.retreatTiles.Add(ReturnBattleTile(new Vector2(xPos - 2, yPos - 1)));
        }

        //Right
        if (xPos + 2 < gridWidth)
        {
            _battleTile.retreatTiles.Add(ReturnBattleTile(new Vector2(xPos + 2, yPos)));
            if (yPos + 1 < gridHeight)
                _battleTile.retreatTiles.Add(ReturnBattleTile(new Vector2(xPos + 2, yPos + 1)));
            if (yPos - 1 >= 0)
                _battleTile.retreatTiles.Add(ReturnBattleTile(new Vector2(xPos + 2, yPos -1)));
        }

    }

    private void SetNeighboursTiles(BattleTile _battleTile)
    {
        float xPos = _battleTile.gridPosition.x;
        float yPos = _battleTile.gridPosition.y;

        //Down
        if (yPos - 1 >= 0)
        {
           _battleTile.neighbourTiles.Add(ReturnBattleTile(new Vector2(xPos, yPos - 1)));
        }

        //Up
        if (yPos + 1 < gridHeight)
        {
            _battleTile.neighbourTiles.Add(ReturnBattleTile(new Vector2(xPos, yPos + 1)));
        }

        if (xPos - 1 >= 0)
        {
            //Left
            _battleTile.neighbourTiles.Add(ReturnBattleTile(new Vector2(xPos - 1, yPos)));
            //Left down
            // if(gridPosition.y - 1 >= 0) 
            //   _neighboursTiles.Add(levelGrid.ReturnBattleTile(new Vector2(gridPosition.x - 1, gridPosition.y - 1)));
            //Left up
            //if (gridPosition.y + 1 < levelGrid.gridHeight) 
            //  _neighboursTiles.Add(levelGrid.ReturnBattleTile(new Vector2(gridPosition.x - 1, gridPosition.y + 1)));
        }

        if (xPos + 1 < gridWidth)
        {
            //Right
            _battleTile.neighbourTiles.Add(ReturnBattleTile(new Vector2(xPos + 1, yPos)));
            //Right down
            //if (gridPosition.y - 1 >= 0) 
            //_neighboursTiles.Add(levelGrid.ReturnBattleTile(new Vector2(gridPosition.x + 1, gridPosition.y - 1)));
            //Right up
            //if (gridPosition.y + 1 < levelGrid.gridHeight) 
            //  _neighboursTiles.Add(levelGrid.ReturnBattleTile(new Vector2(gridPosition.x + 1, gridPosition.y + 1)));
        }
    }

    /// <summary>
    /// Checks if the new position on the grid exists or is available.
    /// </summary>
    /// <param name="_gridNewPos"></param>
    /// <returns></returns>
    public bool CanMoveGrid(Vector2 _gridNewPos)
    {
        battleTiles.TryGetValue(_gridNewPos, out BattleTile _battleTile);
        if (battleTiles.ContainsKey(_gridNewPos) && _battleTile.CompareStateWith(TileState.Empty))
            return true;
        else
            return false;
    }

    public bool CanMoveGrid(Vector2 _gridNewPos, Vector2 _curentGridPos)
    {
        if (battleTiles.ContainsKey(_gridNewPos + _curentGridPos))
            return true;
        else
            return false;
    }


    /// <summary>
    /// Returns the battle tile with a vector2.
    /// </summary>
    /// <param name="_gridPosition"></param>
    /// <returns></returns>
    public BattleTile ReturnBattleTile(Vector2 _gridPosition)//Vector2 _gridPos)
    {
        battleTiles.TryGetValue(_gridPosition, out BattleTile value);
        return value;
        //return battleTiles[_gridPosition];
    }

    /// <summary>
    /// Returns the battle tile with a vector2.
    /// </summary>
    /// <param name="_battleTile"></param>
    /// <returns></returns>
    public Vector2 GetBattleTilePosition(BattleTile _battleTile)
    {
        Vector2 tileVector = Vector2.zero;
        foreach (KeyValuePair<Vector2, BattleTile> _item in battleTiles)
        {
            if (_item.Value == _battleTile)
            {
                tileVector= _item.Key;
            }
        }
        return tileVector;
    }

    
}
