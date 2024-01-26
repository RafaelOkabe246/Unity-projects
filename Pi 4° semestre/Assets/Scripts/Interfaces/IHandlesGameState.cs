using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHandlesGameState 
{
    public void OnChangeGameState(GameState newGameState);
}
