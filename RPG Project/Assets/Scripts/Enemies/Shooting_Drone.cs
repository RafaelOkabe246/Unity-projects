using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting_Drone : MonoBehaviour
{
    public float speed = 3f;
    public float distance;
    public float direction;

    public Vector2 Trajetória;

    private Rigidbody2D Rig;
    private Animator DroneAnimator;

    [SerializeField]
    private bool moveRight = true;

    // public Transform groundDetection;


    [Header("Patrol")]

    private Vector2 direção;
    public LayerMask patrolRange;

    public LayerMask player;
    public bool isPlayer_in_Range;
    public GameObject Shot;
    public Transform Ground_info;
    public Transform Shot_origin;
    public bool isShooting;
    public bool isDead;

    void Start()
    {
        isDead = false;
        Rig = GetComponent<Rigidbody2D>();
        DroneAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        movimentação();

        DroneAnimator.SetBool("isShooting", isShooting);

        Trajetória = new Vector2(direction, Trajetória.y);

    }

    //Direção
    void FixedUpdate()
    {
        moveDrone(Trajetória);
    }

    //Hit
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shot"))
        {
            isDead = true;
            DroneAnimator.SetTrigger("Hit");
        }
    }

    void Stop()
    {
        speed = 0;
    }

    void Dead()
    {
        Destroy(this.gameObject);
    }

    void moveDrone(Vector2 direction)
    {
        if (isDead == false)
        {
            Rig.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
        }
    }

    void movimentação()
    {
        //Trajetória
        if (moveRight == true)
        {
            direção = new Vector2(-1, 0);
            direction = -1f;

        }
        else
        {
            direção = new Vector2(1, 0);
            direction = 1f;
        }

        RaycastHit2D HitInfo = Physics2D.Raycast(Shot_origin.position, direção, distance, player);

        isPlayer_in_Range = HitInfo;

        RaycastHit2D groundinfo_down = Physics2D.Raycast(Ground_info.position, Vector2.down, distance, patrolRange);

        if (groundinfo_down == false)
        {
            flip();
        }

        if(isPlayer_in_Range == true) 
        {
            isShooting = true;
            speed = 0;
        }
        else if(isPlayer_in_Range == false && isShooting == true)
        {
            speed = 0f;
        }
        else if (isPlayer_in_Range == false && isShooting == false)
        {
            speed = 3f;
        }

    }

    void flip()
    {
        moveRight = !moveRight;
        transform.Rotate(0f, 180f, 0f);
    }

    void Continue_Shooting()
    {
        isShooting = true;
    }
    
    void Stop_Shooting()
    {
        isShooting = false;
    }

    void Shooting()
    {
        Instantiate(Shot, Shot_origin.transform.position, transform.rotation);
    }
}
