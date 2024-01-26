using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _tilePrefab;

    [SerializeField] private Transform _cam;

    public Dictionary<Vector2, Tile> _tiles = new Dictionary<Vector2, Tile>();
   // public List<Tile> _tiles = new List<Tile>();

    public Transform tilesParent;

    public PiecesManager piecesManager;

    public Paw pawPrefab;
    public Bishop bishopPrefab;
    public Tower towerPrefab;
    public Horse horsePrefab;


    private void Start()
    {
        piecesManager = FindObjectOfType<PiecesManager>();
        GenerateGr);
        SpawnWhitePieces();
    }

    void SpawnWhitePieces()
    {
        //White pieces
        //Pawns
        for (int x = 0; x < 8; x++)
        {
            Paw paw = Instantiate(pawPrefab, _tiles[new Vector2(x, 1)].transform.position, Quaternion.identity);
            paw.currentPosition = new Vector2(x, 1);
            piecesManager.whitePieces.Add(paw);
        }
        
        //Towers
        Tower tower1 = Instantiate(towerPrefab, _tiles[new Vector2(0, 0)].transform.position, Quaternion.identity);
        Tower tower2 = Instantiate(towerPrefab, _tiles[new Vector2(7, 0)].transform.position, Quaternion.identity);
        tower1.currentPosition = new Vector2(0, 0);
        tower2.currentPosition = new Vector2(7, 0);
        piecesManager.whitePieces.Add(tower1);
        piecesManager.whitePieces.Add(tower2);


        //Bishops
        Bishop bishop1 = Instantiate(bishopPrefab, _tiles[new Vector2(2, 0)].transform.position, Quaternion.identity);
        Bishop bishop2 = Instantiate(bishopPrefab, _tiles[new Vector2(5, 0)].transform.position, Quaternion.identity);
        bishop1.currentPosition = new Vector2(2, 0);
        bishop2.currentPosition = new Vector2(5, 0);
        piecesManager.whitePieces.Add(bishop1);
        piecesManager.whitePieces.Add(bishop2);

        //Horses
        Horse horse1 = Instantiate(horsePrefab, _tiles[new Vector2(1, 0)].transform.position, Quaternion.identity);
        Horse horse2 = Instantiate(horsePrefab, _tiles[new Vector2(6, 0)].transform.position, Quaternion.identity);
        horse1.currentPosition = new Vector2(1, 0);
        horse2.currentPosition = new Vector2(6, 0);
        piecesManager.whitePieces.Add(horse1);
        piecesManager.whitePieces.Add(horse2);

        //Queen and king
        //Paw king = Instantiate()
    }


    void GenerateGrid()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity, tilesParent);
                spawnedTile.name = $"Tile: {x} {y}";
                

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset, new Vector2(x,y));

                _tiles[new Vector2(x, y)] = spawnedTile;
            }
        }

        //tilesParent.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);
    }

    public bool CanMoveToTile(Vector2 _tilePos, Piece _piece)
    {
        Tile checkedTile = _tiles[_tilePos];
        Debug.Log(checkedTile);
        if(checkedTile.currentPiece.pieceColor == _piece.pieceColor || checkedTile.currentPiece == _piece || checkedTile == null)
        {
            return false;

        }
        return true;
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if(_tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }
        return null;
    }
}
