using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHideObject : interactiveObject
{
    public void HidePlayer()
    {
        _player.HideShow();
    }
}
