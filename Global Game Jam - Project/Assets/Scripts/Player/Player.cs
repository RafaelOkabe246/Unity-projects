using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public enum Modos
{
    Ativo,
    Passivo,

};

public class Player : MonoBehaviour
{
    [Header("Players scripts")]
    [SerializeField] internal Players_Controller Controller;
    [SerializeField] internal Player_inputs Inputs;
    [SerializeField] internal Player_movement Movement;
    [SerializeField] internal Player_Collisions Collisions;
    [SerializeField] internal Passive_mode Passivo;
    [SerializeField] internal Player_diagloue Diagloue;


    [Header("Estados")]
    public Modos Estado_atual;
    [SerializeField] internal Transform Respawn;


    [Header("Components")]
    //Components 
    [SerializeField]
    internal Rigidbody2D Rig;
    [SerializeField]
    internal Animator Anim;



    [Header("Player s movement")]
    //Movement variables
    [SerializeField] internal float Vertical_direction;
    public Vector2 Horizontal_direction;
    public float speed;
    public float JumpForce = 5f;
    public Transform groundCheckPosition;


    [Header("Ladder")]
    public LayerMask Water;
    public LayerMask Ladder;
    public LayerMask Ground;

    [Header("Collisions")]
    [SerializeField] internal Alavanca _Alavanca;


    [Header("Cloud talk")]
    [SerializeField] internal Transform CloudTalk;

    //Type of Player
    [SerializeField] internal bool isLouis;
    [SerializeField] internal bool isClara;
    [SerializeField] internal bool isJoana;

    //Passive script

    [SerializeField] internal bool isPassivo;




    private void Start()
    {
        Rig = this.GetComponent<Rigidbody2D>();
        Anim = this.GetComponent<Animator>();

        CloudTalk = this.gameObject.transform.GetChild(1);

        transform.position = Respawn.position;
    }

    private void Update()
    {
        if (Estado_atual == Modos.Passivo)
        {
            isPassivo = true;
        }
        else if (Estado_atual == Modos.Ativo)
        {
            isPassivo = false;
        }
    }


}
