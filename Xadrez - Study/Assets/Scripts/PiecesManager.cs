using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PiecesManager : MonoBehaviour
{
    Stack<ICommand> commandsList;
    public List<Piece> whitePieces;
    public List<Piece> blackPieces;


    public PiecesManager()
    {
        commandsList = new Stack<ICommand>();
    }

    private void Start()
    {
        //UpdatePiecesPotentialMoves();
        Invoke(nameof(UpdatePiecesPotentialMoves), 3f);
    }

    public void UpdatePiecesPotentialMoves()
    {
        //Debug.Log("TEste");
        foreach (Piece piece in whitePieces)
        {
          //  Debug.Log(piece);
            piece.UpdatePotentialMoves();
        }
    }

    public void AddCommand(ICommand newCommand)
    {
        newCommand.Execute();
        commandsList.Push(newCommand);
    }
}
