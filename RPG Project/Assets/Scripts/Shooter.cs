using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public float speed;
    public float distance;
    public float direction;

    public float range;
    public GameObject shot;
    public Transform ShotOrigin;

    public Vector2 Trajetória;

    private Rigidbody2D Rig;
    private Animator DroneAnimator;

    [SerializeField]
    private bool moveRight = true;

    public Transform groundDetection;
    public Transform player;

    public LayerMask patrolRange;
    public LayerMask target;

    void Start()
    {
        Rig = GetComponent<Rigidbody2D>();
        DroneAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        movimentação();

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

    void Attacking()
    {
        GameObject Shooting = Instantiate(shot, ShotOrigin.position, ShotOrigin.rotation);
        Destroy(Shooting, 0.8f);
    }

    void movimentação()
    {
        RaycastHit2D groundinfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance, patrolRange);

        if (groundinfo == false)
        {
            if (moveRight == true)
            {
                moveRight = false;
            }
            else
            {
                moveRight = true;
            }
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
