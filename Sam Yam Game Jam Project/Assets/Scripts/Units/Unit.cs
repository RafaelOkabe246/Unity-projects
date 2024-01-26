using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Faction unitFaction;


    [Header("Stats")]
    public UnitStats _unitStats;
    public int _HP;
    public int _Attack;
    public int _Defense;
    


    public SpriteRenderer _sprite;

    [Header("Actions")]
    public bool HasActed;
    public Tile OccupiedTile;
    public Vector2 currentPosition;
    public List<Tile> PathPositions;

    private void Start()
    {
        _HP = _unitStats.HP;
        _Attack = _unitStats.Attack;
        _Defense = _unitStats.Defense;
        HasActed = false;
        
    }


}
public enum Faction
{
    PLAYER,
    ENEMY
}