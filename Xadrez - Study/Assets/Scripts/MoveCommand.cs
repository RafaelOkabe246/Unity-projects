using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ICommand
{

    public Piece piece;
    public Transform previousPos;
    public Transform newPos;

    public MoveCommand(Transform _previousPos, Transform _newPos, Piece _piece)
    {
        piece = _piece;
        previousPos = _previousPos;
        newPos = _newPos;
    }


    public void Execute()
    {
        piece.transform.position = newPos.position;
    }

    public void Undo()
    {
        piece.transform.position = previousPos.position;
    }
}
