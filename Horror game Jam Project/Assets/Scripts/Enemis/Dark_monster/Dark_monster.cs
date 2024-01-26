using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dark_monster : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 3f;
    private Rigidbody2D Rig;

    [Header("Direções")]
    public bool Vertical;
    public bool Horizontal;
    
    //Horizontal
    public bool isMovingRight;
    public Transform RightCheck;
    public Transform LeftCheck;

    public bool Direita;
    public bool Esquerda;

    //Vertical
    public bool isMovingTop;
    public Transform TopCheck;
    public Transform DownCheck;

    public bool Topo;
    public bool baixo;

    [SerializeField]
    private Vector2 Direction;
    public LayerMask Parede;

    [SerializeField] private Player _Player;
    [SerializeField] private AudioSource Steps;

    void Start()
    {
        Steps = this.GetComponent<AudioSource>();
        _Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Rig = this.GetComponent<Rigidbody2D>();

        if (Vertical)
        {
            Direction = Vector2.up;
            isMovingTop = true;
        }
        else if (Horizontal)
        {
            Direction = Vector2.right;
            isMovingRight = true;
        }
    }


    void Update()
    {
        Checking_walls();
        if (_Player.Dead == true)
        {
            Steps.Stop();
        }
    }

    void movement(Vector2 direction)
    {
        //transform.position = Vector2.MoveTowards(transform.position, direction, speed * Time.deltaTime);
        //Rig.MovePosition((Vector2)Rig.position + direction);
        
        Rig.velocity = direction * speed;
    }

    private void FixedUpdate()
    {
        movement(Direction);

        Topo = Physics2D.OverlapCircle(TopCheck.position, 0.2f, Parede);
        baixo = Physics2D.OverlapCircle(DownCheck.position, 0.02f, Parede);

        Esquerda = Physics2D.OverlapCircle(LeftCheck.position, 0.2f, Parede);
        Direita = Physics2D.OverlapCircle(RightCheck.position, 0.02f, Parede);
    }


    void Checking_walls()
    {
        if (Vertical)
        {

            if (Topo == true)
            {
                //Find_new_y_direction();
                isMovingTop = false;
                Direction = Vector2.down;
            }
            else if (baixo == true)
            {
                //Find_new_y_direction();
                isMovingTop = true;
                Direction = Vector2.up;
            }

        }

        if (Horizontal)
        {
            if (Direita == true)
            {
                isMovingRight = false;
                Direction = Vector2.left;
            }
            else if (Esquerda == true)
            {
                isMovingRight = true;
                Direction = Vector2.right;
            }
        }


    }

}
