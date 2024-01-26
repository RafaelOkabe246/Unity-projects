using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalker : Enemy
{
    [SerializeField]
    protected bool isFollowing;

    protected override void Movement(Vector2 direction)
    {
        base.Movement(direction);
    }

    private void FixedUpdate()
    {
        Movement(direction);
    }

    private void Update()
    {
        trajetoria();
    }

    protected override void trajetoria()
    {
        base.trajetoria();

        //Player info
        //RaycastHit2D IsPlayer_in_Range = Physics2D.Raycast(transform.position, direction.normalized, View_area, Player);

        //Ground info
        RaycastHit2D groundinfo_down = Physics2D.Raycast(Ground_check.position, Vector2.down, 3f, Plataform);
        if (groundinfo_down == false)
        {
            flip();
        }

        //if (IsPlayer_in_Range == true)
        //{

        //}
       // else if (IsPlayer_in_Range == false)
       // {

       // }
    }


}
