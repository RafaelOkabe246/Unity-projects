using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D PlayerRG;
    private Animator Player_animator;
    private Game_Controller _GameController;

    public float speed;
    public float Jumpforce;

    [Header("Dash variables")]
    private float dashTime;
    public float dashSpeed;
    public float startDashtime;
    private int direction;

    public Transform GroundCheck;

    public GameObject Lastcheckpoint;

    public bool isLookleft;
    private bool isgrounded;
    public bool isTrevo;


    void Start()
    {
        PlayerRG = GetComponent<Rigidbody2D>();
        Player_animator = GetComponent<Animator>();
        dashTime = startDashtime;

        _GameController = FindObjectOfType(typeof(Game_Controller)) as Game_Controller;
        _GameController.PlayerTransform = this.transform;
    }

    void Update()
    {
        Move_and_jump();
        //Dash();

    }


    //Funções próprias
    void Move_and_jump()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float speedY = PlayerRG.velocity.y;

        if (h > 0 && isLookleft == true)
        {
            Flip();
        }
        else if (h < 0 && isLookleft == false)
        {
            Flip();
        }

        if (Input.GetButtonDown("Jump") && isgrounded == true)
        {
            PlayerRG.AddForce(new Vector2(0, Jumpforce));
        }

        PlayerRG.velocity = new Vector2(h * speed, speedY);
        Player_animator.SetInteger("h", (int) h);
        Player_animator.SetBool("isgrounded", isgrounded);
        Player_animator.SetFloat("speedY", speedY);
    }

    void Flip()
    {
        isLookleft = !isLookleft;
        float x = transform.localScale.x * -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

    void Dash()
    {
        if(direction == 0)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                direction = 1;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                direction = 2;
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                direction = 3;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                direction = 4;
            }
            else 
            {
                if (dashTime <= 0)
                {
                    direction = 0;
                    dashTime = startDashtime;
                    PlayerRG.velocity = Vector2.zero;
                }
                else
                {
                    dashTime -= Time.deltaTime;

                    if(direction == 1)
                    {
                        PlayerRG.velocity = Vector2.left * dashSpeed;
                    }
                    else if(direction == 2)
                    {
                        PlayerRG.velocity = Vector2.right * dashSpeed;
                    }
                    else if (direction == 3)
                    {
                        PlayerRG.velocity = Vector2.up * dashSpeed;
                    }
                    else if (direction == 4)
                    {
                        PlayerRG.velocity = Vector2.down * dashSpeed;
                    }
                }
            } 
        }
    }

    //Colisões
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Checkpoint"))
        {
            Lastcheckpoint = coll.gameObject;
        }
        if (coll.gameObject.layer == 9)
        {
            transform.position = Lastcheckpoint.transform.position;
        }
        if (coll.gameObject.layer == 10)
        {
            isTrevo = true;
            Trevo();
        }
        if (coll.gameObject.tag == "NewLevel")
        {
            _GameController.Phases += 1;
            Destroy(coll.gameObject);
        }

    }

    void Trevo()
    {
        float Transform_time = 10f;

        if(Transform_time > 0)
        {
            Transform_time = -1 * Time.deltaTime;
        }
        else if(Transform_time <= 0)
        {
            isTrevo = false;
        }

        

        Player_animator.SetBool("isTrevo", isTrevo);
        Player_animator.SetFloat("Transform_time", Transform_time);

    }


    //Ground check
    void FixedUpdate()
    {
        isgrounded = Physics2D.OverlapCircle(GroundCheck.position, 0.02f);
    }
}
