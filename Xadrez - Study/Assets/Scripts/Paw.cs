using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Paw : Piece
{
    public override List<Tile> CheckPossibleMoves()
    {
        List<Tile> moves = new List<Tile>();

        //Vertical
        Vector2 verticalMove1 = new Vector2(currentPosition.x, currentPosition.y + 1);
        if (gridManager.CanMoveToTile(verticalMove1, this))
            moves.Add(gridManager._tiles[verticalMove1]);
            //moves.Add(gridManager.GetTileAtPosition(verticalMove1));

        Vector2 verticalMove2 = new Vector2(currentPosition.x, currentPosition.y + 2);
        if (currentPosition.y == 1 && pieceColor == PieceColor.White && gridManager.CanMoveToTile(verticalMove2, this))
            moves.Add(gridManager._tiles[verticalMove2]);
        //moves.Add(gridManager.GetTileAtPosition(verticalMove2));

        Debug.Log(verticalMove1 + verticalMove2);

        //Diagonal

        Vector2 diagonalRight = gridManager.GetTileAtPosition(new Vector2(currentPosition.x + 1, currentPosition.y + 1)).gridPosition;
        Vector2 diagonalLeftt = gridManager.GetTileAtPosition(new Vector2(currentPosition.x - 1, currentPosition.y + 1)).gridPosition;

        if (gridManager._tiles.ContainsKey(diagonalRight) && gridManager.CanMoveToTile(diagonalRight, this))
        {
            moves.Add(gridManager._tiles[diagonalRight]);
        }
        else if (gridManager._tiles.ContainsKey(diagonalLeftt) && gridManager.CanMoveToTile(diagonalLeftt, this))
        {
            moves.Add(gridManager._tiles[diagonalLeftt]);
        }

        return moves;
    }
}
