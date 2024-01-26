using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public enum PieceColor
{
    White,
    Black
}


public abstract class Piece : MonoBehaviour, IInteractable
{
    public PieceColor pieceColor;
    public Vector2 currentPosition;

    public PiecesManager pieceManager;
    public GridManager gridManager;

    public List<Tile> possibleMovesTiles;


    private void Start()
    {
        pieceManager = FindObjectOfType<PiecesManager>();
        gridManager = FindObjectOfType<GridManager>();

    }

    public void Die()
    {

    }

    public void MoveToNewTile(Tile newPos)
    {
        transform.position = newPos.gridPosition;
        currentPosition = newPos.gridPosition;
        newPos.currentPiece = this;
    }
    

    public abstract List<Tile> CheckPossibleMoves();

    public void UpdatePotentialMoves()
    {
        possibleMovesTiles = CheckPossibleMoves();
    }

    public void Interact()
    {
        
    }
}
