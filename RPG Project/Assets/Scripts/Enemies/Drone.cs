    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public float speed;
    public float distance;
    public float direction;

    public Vector2 Trajetória;

    private Rigidbody2D Rig;
    private Animator DroneAnimator;

    [SerializeField]
    private bool moveRight = true;

   // public Transform groundDetection;

    public LayerMask patrolRange;

    void Start()
    {
        Rig = GetComponent<Rigidbody2D>();
        DroneAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        movimentação();

        Trajetória = new Vector2(direction,Trajetória.y);
    }

    //Direção
    void FixedUpdate()
    {
        moveDrone(Trajetória);
    }

    //Hit
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Shot"))
        {

            DroneAnimator.SetTrigger("Hit");
        }
    }

    
    void Dead()
    {
        Destroy(this.gameObject);
    }

    void moveDrone(Vector2 direction)
    {
        Rig.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }

    void movimentação()
    {
        RaycastHit2D groundinfo_down = Physics2D.Raycast(transform.position, Vector2.down, distance, patrolRange);

        if (groundinfo_down == false)
        {
            moveRight = !moveRight;
        }

        //Trajetória
        if (moveRight == true)
        {
            direction = -1f;
        }
        else
        {
            direction = 1f;
        }
    }
}

