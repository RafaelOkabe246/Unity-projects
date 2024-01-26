using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    protected Rigidbody2D rig;
    protected Animator anim;

    public bool isAlive;

    public Vector2 Direction;
    public float Speed;

    protected void Start()
    {
        isAlive = true;
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
    }

    protected void FixedUpdate()
    {
        if(isAlive == true)
        {
            Movement(Vector2.left, Speed);
        }
    }

    protected void Movement(Vector2 direction, float speed)
    {
        rig.MovePosition((Vector2)transform.position + direction * speed * Time.deltaTime);
        //rig.velocity = direction * speed;
    }

    protected void Attack(float damage)
    {
        anim.SetFloat("AttackDamage", damage);
        anim.SetTrigger("Attack");
    }
}
