using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Move to right and left in a limited space ground
*/

public class EnemyPatrol : Enemy
{
    public LayerMask collisionsLayers;
    [SerializeField] protected bool canChangeDir = true;
    public Transform checkHorizontal;
    public Transform checkVertical;
    [SerializeField] protected bool isGrounded;
    public float groundCheckDistance;
    public float horizontalCheckDistance;

    protected void FixedUpdate()
    {
       Movement();
       CheckCollisions();
    }
    protected void Update()
    {
        SetAnimations();
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(checkVertical.position, new Vector2(checkVertical.position.x, checkVertical.position.y - 1f));
        Gizmos.DrawLine(checkHorizontal.position, new Vector2(checkHorizontal.position.x + horizontalCheckDistance * dir, checkHorizontal.position.y));
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        Play();
    }

    protected void SetAnimations()
    {
        anim.SetBool("isGrounded", isGrounded);
    }

    protected void CheckCollisions()
    {
        RaycastHit2D hitHorizontal = Physics2D.Raycast(checkHorizontal.position, Vector2.right * dir, horizontalCheckDistance, collisionsLayers);
        RaycastHit2D hitVertical = Physics2D.Raycast(checkVertical.position, Vector2.down, 1f, collisionsLayers);
        RaycastHit2D hitGround = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, collisionsLayers);
        isGrounded = hitGround;

        if (hitVertical == false && canChangeDir && isGrounded || hitHorizontal == true && canChangeDir && isGrounded)
        {
            canChangeDir = false;
            dir = dir * -1;
        }
    }

    protected void Movement()
    {

        if (rig.velocity.x > 0 && !isLookingRight && isGrounded)
        {
            Flip();
        }
        else if (rig.velocity.x < 0 && isLookingRight && isGrounded)
        {
            Flip();
        }
        
        if (!isGrounded || isPaused)
            speed = 0;
        else if(isGrounded && !isPaused)
            speed = maxSpeed;
        
         rig.velocity = new Vector2(dir * speed , rig.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Lodo"))
        {
            TakeDamage();
        }
    }

    protected override void Flip()
    {
        base.Flip();
        canChangeDir = true;
    }

    protected override void Play()
    {
        coll.enabled = true;
        speed = maxSpeed;
        rig.constraints = RigidbodyConstraints2D.None;
        rig.constraints = RigidbodyConstraints2D.FreezeRotation;
        isPaused = false;
        canChangeDir = true;
    }

    protected override void Pause()
    {
        speed = 0f;
        coll.enabled = false;
        rig.constraints = RigidbodyConstraints2D.FreezeAll;
        isPaused = true;

    }
}
