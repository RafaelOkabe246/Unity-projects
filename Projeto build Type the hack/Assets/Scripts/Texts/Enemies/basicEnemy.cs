using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicEnemy : Enemy
{
    [SerializeField] protected Transform target;

    private void Start()
    {
        target = player.transform;
    }


    void MoveToPlayer()
    {
        //Move 
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        //Change the sprite to face the player
        spr.transform.up = target.position - spr.transform.position;
    }

    void FixedUpdate()
    {
        if (!cantMove)
            MoveToPlayer();
    }

}
