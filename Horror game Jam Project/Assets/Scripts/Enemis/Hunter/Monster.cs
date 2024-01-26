using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private AudioSource Steps;
    private Animator _Animator;
    private Player _Player;
    public bool isMoving;

    public Transform spawn;

    [Header("Finding the player")]

    public bool Seeing_the_wall;
    public bool isSeeing;    
    public LayerMask Player;

    public LayerMask Parede;

    public float distance_to_player;

    void Start()
    {
        _Player = FindObjectOfType(typeof(Player)) as Player;
        _Animator = GetComponent<Animator>();
        Steps = GetComponent<AudioSource>();
        distance_to_player = 10f;

        //Physics2D.queriesStartInColliders = false;
    }


    void Update()
    {
       Movement();
        sound();
    }

    public void Movement()
    {
        //Checking_walls();
        Distance_to_Player();

        if (isSeeing == true && _Player.LightOn == true )
            {
                Steps.mute = false;
                isMoving = true;
                _Animator.SetBool("isFollowing", true);
            }
            else if (_Player.LightOn == false)
            {
            
                Steps.mute = true;
                isMoving = false;
                _Animator.SetBool("isFollowing", false);
            }

    }

    public void sound()
    {
        if(_Player.Dead == true)
        {
            Steps.mute = true;
        }
    }

    void Distance_to_Player()
    {
        //Seeing_the_wall = Physics2D.Raycast(transform.position, _Player.transform.position, distance_to_player, Parede);
        isSeeing = Physics2D.Raycast(transform.position, _Player.transform.position, distance_to_player, Player );

        if (Vector2.Distance(transform.position, _Player.transform.position) < distance_to_player)
        {
            isSeeing = true;
        }

    }

    void Checking_walls()
    {
        RaycastHit2D HitInfo = Physics2D.Raycast(transform.position, _Player.transform.position);

        if (HitInfo.collider.CompareTag("Walls"))
        {
            Debug.DrawLine(transform.position, HitInfo.point, Color.red);
            Seeing_the_wall = true;
        }
        else 
        {
            Seeing_the_wall = false;
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isSeeing = true;
        }

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isSeeing = false;
        }
    }
}
