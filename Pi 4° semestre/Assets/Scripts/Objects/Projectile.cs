using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


public class Projectile : ObjectTile
{
    [SerializeField] private List<BattleTile> path = new List<BattleTile>();
    public int currentTileIndex;

    public SpriteRenderer gfx;

    public bool canMove;

    [SerializeField] private float dir;
    public int damage;
    public float moveSpeed;
    Graph g = new Graph();

    [SerializeField] private Animator anim;

    public string[] damagebleTags;

    public bool canDoDamage;

    public void Initialize(Grid _levelGrid, BattleTile _spawnTile, float _dir)
    {
        canDoDamage = true;
        dir = _dir;
        levelGrid = _levelGrid;
        currentTile = _spawnTile;
        currentGridPosition = currentTile.gridPosition;
        transform.position = currentTile.transform.position;

        //0 ou gridWidth.x -1
        float xPos = 0;
        if (dir < 0)
            xPos = 0;
        else
            xPos = levelGrid.gridWidth - 1;

        Vector2 targetPos = new Vector2(xPos, currentGridPosition.y);
        gfx.flipX = dir > 0;
        path = g.AStarIgnoreOccupiedTiles(levelGrid, currentTile, levelGrid.ReturnBattleTile(targetPos));
        path.Reverse();
        canMove = true;
    }


    private void Update()
    {
        Move();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //float timerToDestroy = 0;
        //timerToDestroy += Time.deltaTime;

        if(canDoDamage)
        {
            if (damagebleTags.Contains(collision.tag))//.OnCurrentTile() == currentTile)
            {
                canMove = false;
                collision.TryGetComponent<IDamageable>(out IDamageable damageable);
                damageable.TakeDamage(damage);
                canDoDamage = false;
                TriggerOnDestroyed();
            }
        }
        
    }


    void Move()
    {
        if (canMove)
        {
            //If arrived destination
            if (currentTile == path[path.Count - 1])
            {
                TriggerOnDestroyed();
                canMove = false;
                return;
            }

            //Check if current position exists
            if (path[currentTileIndex + 1] != null)
                transform.position = Vector2.MoveTowards(transform.position, path[currentTileIndex + 1].transform.position, moveSpeed * Time.deltaTime);

            //Update new position to move
            if (Vector2.Distance(transform.position, path[currentTileIndex + 1].transform.position) < 0.05f)
            {
                //Update new tile pos
                currentTileIndex++;
                currentTile = path[currentTileIndex];
                currentGridPosition = currentTile.gridPosition;
            }
        }
    }    

    void TriggerOnDestroyed()
    {
        
            anim.SetTrigger("destroy");

    }

    protected override void OnDestroyed()
    {
        gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}
