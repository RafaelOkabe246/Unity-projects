using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : InteractiveObject
{
    public bool isActivate;
    public Rigidbody2D rig;
    public SpriteRenderer gfx;

    protected string tagCollidePlayer = "Obstacle";
    protected string tagIgnorePlayer = "NoInteractable";
    protected LayerMask groundLayer = 6;
    protected LayerMask ignoreLayer = 10;

}
