using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public TerrainLayout levelLayout;

    public List<TerrainType> Terrains;

    public static GridManager instance;
    public int _width, _height;

    [SerializeField] private Tile _baseTile;

    [SerializeField] private Transform _cam;

    public Dictionary<Vector2, Tile> _tiles;

    public Tile selectedTile;

    private void Awake()
    {
        instance = this; 
    }

    public void GeneratorGrid()
    {
        _width = levelLayout.width;
        _height = levelLayout.height;

        _tiles = new Dictionary<Vector2, Tile>();

        for (int x = 0; x < _width; x++)
        {
            for(int y = 0; y < _height; y++)
            {

                var spawnedTile = Instantiate(_baseTile, new Vector3(x,y), Quaternion.identity);
                spawnedTile.name = $"Tile {x}" + ", " + $"{y}";

                _tiles[new Vector2(x, y)] = spawnedTile;

                CustomTiles(x, y, spawnedTile);

                spawnedTile.InializatingTile(x, y);

                //Implantando as unidades
                UnitManager.instance.PlaceUnit(spawnedTile);

            }
        }
        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height/2 -0.5f, -10);
        GameManager.instance.ChangeState(GameState.PlayerTurn);
    }


    public void CustomTiles(int x, int y, Tile baseTile)
    {
        //Check the terrains
        for (int i = 0; i < levelLayout._terrainScript.Count; i++)
        {
            //Check the position the terrains occupy
            for (int terrainVectorIndex = 0; terrainVectorIndex < levelLayout._terrainScript[i]._terrainVector.Count; terrainVectorIndex++)
            {
                //Verify if the tile has the same vector
                if (levelLayout._terrainScript[i]._terrainVector[terrainVectorIndex] == new Vector2(x,y))
                {
                    //Add the terrain type according to the index
                    baseTile._terrainType = Terrains[levelLayout._terrainScript[i]._terrainIndex];
                }
            }
        }
    }
}
