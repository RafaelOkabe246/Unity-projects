using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectTile : MonoBehaviour
{
    public bool canTakeDamage;
    public bool isDestroyed;
    public Grid levelGrid;
    public Vector2 currentGridPosition;
    public BattleTile currentTile;

    public delegate void ObjectDestroyed();
    public ObjectDestroyed objectDestroyed;
    public virtual void Initialize()
    {

    }
    protected virtual void OnDestroyed()
    {
        currentTile.ClearTile();
        gameObject.SetActive(false);
    }

    public void SetCanTakeDamage(bool i)
    {
        canTakeDamage = i;
    }



}
