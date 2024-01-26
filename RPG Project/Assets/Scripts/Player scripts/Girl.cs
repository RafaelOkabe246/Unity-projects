using System.Collections;
using UnityEngine;

public class Girl : MonoBehaviour
{
    [Header("Health")]
    public Color HitColor;
    public Color NormalColor;
    private SpriteRenderer _SR;
    public HealthBar healthbar;

    [Header("Rig and Transform")]
    public Transform under_girl;
    private Rigidbody2D Rig;
    private Animator PlayerAnimator;
    public float life;
    public float maxLife = 100f;


    
    [Header("Reset checkpoint")]
    public Transform Reset_checkpoint;
    private float maxReset = 1f;
    private float startReset = 0;
    private float currentReset;

    //Dead
    [Header("Checkpoints")]
    public Transform Last_Checkpoint;

    //Cutscene
    [Header("Cutscene")]
    private CutsceneTrigger _CutsceneTrigger;
    private RuntimeAnimatorController _controlador;
    public bool Cannot_do;

    //Movement
    [Header("Movement")]
    public float speed;
    public float jumpforce = 6f;
    public float Super_jumpforce = 15f;
    [SerializeField]
    private float aircontrol;
    public static bool canMove;
    private Collider2D Girl_collider;

    public Transform upCheck;
    public Transform groundCheck;
    [SerializeField]
    private bool isGrounded;

    public bool islookleft;
    public bool isattack;

    public bool Location;

    //Position
    [Header("Position")]
    float Save_position_time = 0.1f;
    float Start_position_time = 0f;
    float Current_position_time;
    public Transform Position;

    //Dash
    [Header("Dash variables")]
    public float dashSpeed;
    [SerializeField]
    private float dashTime;
    public float startDashTime;
    [SerializeField]
    private int direction;
    public float dashDelay;
    public GameObject DashEffect;


    //Shot
    [Header("Shot variables")]
    public Transform ShotOrigin;
    public GameObject shot;
    public float Shoot_delay;

    //Ladder
    [Header("Ladder")]
    public float CheckRadius = 0.3f;
    public LayerMask Whatisladder;
    public bool isClimbing;
    public float climbing_speed;
    public float inputvertical;


    public LayerMask Plataform;

    void Start()
    {
        life = maxLife;

        Girl_collider = this.GetComponent<Collider2D>();
        canMove = true;
        Rig = GetComponent<Rigidbody2D>();
        PlayerAnimator = GetComponent<Animator>();
        dashTime = startDashTime;
    }

    void Update()
    {
        //Vida
        healthbar.health = life;
        healthbar.maxHealth = maxLife;

        //Dash
        if (direction == 0)
        {
            if (dashDelay < 3)
            {
                dashDelay += Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) && dashDelay >= 3)
            {
                //Instantiate(DashEffect, transform.position, Quaternion.identity);
                direction = 1;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && dashDelay >= 3)
            {
                //Instantiate(DashEffect, transform.position, Quaternion.identity);
                direction = 2;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && dashDelay >= 3)
            {
                //Instantiate(DashEffect, transform.position, Quaternion.identity);
                direction = 3;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && dashDelay >= 3)
            {
                //Instantiate(DashEffect, transform.position, Quaternion.identity);
                direction = 4;
            }
        }
        else
        {
            dashDelay = 0;
            if (dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                Rig.velocity = Vector2.zero;
            }
            else
            {
                dashTime -= Time.deltaTime;
                //PlayerAnimator.SetTrigger("Shake");
                if (direction == 1)
                {
                    Rig.velocity = Vector2.left * dashSpeed;
                }
                else if (direction == 2)
                {
                    Rig.velocity = Vector2.right * dashSpeed;
                }
                else if (direction == 3)
                {
                    Rig.velocity = Vector2.up * dashSpeed;
                }
                else if (direction == 4)
                {
                    Rig.velocity = Vector2.down * dashSpeed;
                }
            }
        }

        //attack
        if (Input.GetKeyDown(KeyCode.RightShift) && isattack == false && Shoot_delay >= 0.5)
        {
            PlayerAnimator.SetTrigger("attack");
            isattack = true;
        }
        else if (Shoot_delay < 0.5)
        {
            Shoot_delay += Time.deltaTime;
        }


        //Jump
        float speedY = Rig.velocity.y;
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            Jump();
        }


        //Movement
        float h = Input.GetAxisRaw("Horizontal");
        Vector2 dir = new Vector2(h, speedY);
        Horizontal_Vertical_inputs(dir);

        //Dialog
        if (!canMove)
        {
            PlayerAnimator.SetInteger("h", (int)0);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            return;
        }

        //Dead 
        if (life <= 0)
        {
            transform.position = Last_Checkpoint.position;
            life = 50;
        }

        //Animator variables
        PlayerAnimator.SetInteger("h", (int)h);
        PlayerAnimator.SetBool("isGrounded", isGrounded);
        PlayerAnimator.SetFloat("speedY", speedY);
        PlayerAnimator.SetBool("isAttack", isattack);
        PlayerAnimator.SetBool("isClimbing", isClimbing);


    }

    void FixedUpdate()
    {
        //Check location
        float D = 0;
        if (D >= 2)
        {
            Location = true;
        }
        else
        {
            D += Time.deltaTime;
        }

        //Check grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f, Plataform);
        ClimbingLadder();

        //Save postion
        if (isGrounded == true)
        {
            Current_position_time = Start_position_time;

            Current_position_time += Time.deltaTime;

            if (Current_position_time >= Save_position_time)
            {
                Position.position = this.gameObject.transform.position;
            }
        }
        else
        {
            Position = null;
        }

        //Atualizando o reset checkpoint
        Reseting_checkpoint();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Interactive") && Input.GetKeyDown(KeyCode.E) == true)
        {

        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Damage"))
        {
            Damage();

            this.gameObject.layer = LayerMask.NameToLayer("Player");
            PlayerAnimator.SetTrigger("Hit");
            life += -5;
        }
        else if (collision.gameObject.CompareTag("Checkpoint"))
        {
            Last_Checkpoint = collision.gameObject.transform;

        }
        else if (collision.gameObject.CompareTag("Dead"))
        {
            transform.position = Last_Checkpoint.position;
        }
    }

    void Reseting_checkpoint()
    {
        currentReset = startReset;

        if(currentReset < maxReset)
        {
            currentReset += Time.deltaTime;
        }
        else if(currentReset >= maxReset)
        {
            Reset_checkpoint.position = transform.position;
            currentReset = startReset;
        }

    }


    IEnumerator Damage()
    {

        _SR.color = HitColor;
        yield return new WaitForSeconds(0.2f);
        _SR.color = NormalColor;
        yield return new WaitForSeconds(0.2f);
        _SR.color = HitColor;
        yield return new WaitForSeconds(0.2f);
        _SR.color = NormalColor;
        yield return new WaitForSeconds(0.2f);

        this.gameObject.layer = LayerMask.NameToLayer("Player");
    }


    private void Horizontal_Vertical_inputs(Vector2 dir)
    {
        if (canMove == true)
        {

            if (isGrounded == true)
            {
                speed = 6;
            }
            else
            {
                speed -= Time.deltaTime;
            }


            //Turn left and right
            if (dir.x > 0 && islookleft == true)
            {
                flip();
            }
            else if (dir.x < 0 && islookleft == false)
            {
                flip();
            }
            Rig.velocity = (new Vector2(dir.x * speed, Rig.velocity.y));

        }
        else
        {
            return;
        }

        inputvertical = Input.GetAxisRaw("Vertical");
    }


    //Jump voids
    private void Jump()
    {
        Rig.velocity = new Vector2(Rig.velocity.x, 0);
        Rig.velocity += Vector2.up * jumpforce;
    }


    void flip()
    {
        islookleft = !islookleft;
        //float x = transform.localScale.x * -1;
        //transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
        transform.Rotate(0f, 180f, 0f);
    }


    //Ladders
    bool LadderCheck()
    {
        return Girl_collider.IsTouchingLayers(Whatisladder);
    }

    void ClimbingLadder()
    {
        bool up = Physics2D.OverlapCircle(transform.position, CheckRadius, Whatisladder);
        bool down = Physics2D.OverlapCircle(under_girl.position + new Vector3(0, -1), CheckRadius, Whatisladder);

        if (inputvertical != 0 && LadderCheck())
        {
            isClimbing = true;
            Rig.isKinematic = true;
            canMove = false;
        }

        if (isClimbing)
        {
            if (!down && inputvertical <= 0)
            {
                FinishClimbing();
                return;
            }
            if (!up && inputvertical >= 0)
            {
                FinishClimbing();
                return;
            }

            float y = inputvertical * climbing_speed;
            Rig.velocity = new Vector2(0, y);
        }
    }

    void FinishClimbing()
    {
        isClimbing = false;
        Rig.isKinematic = false;
        canMove = true;
    }

    public void Cant_fight_or_move()
    {
        Cannot_do = true;
    }

    public void Can_fight_or_move()
    {
        Cannot_do = false;
    }

    void Fire()
    {
        if (Shoot_delay >= 0.5 && Cannot_do == false)
        {
            Shoot_delay = 0;
            Instantiate(shot, ShotOrigin.position, ShotOrigin.rotation);
        }

    }

    void OnEndAttack()
    {
        isattack = false;
    }

    public void Dialog_Cutscene_Stop()
    {
        canMove = false;
    }
    public void Dialog_Cutscene_Move()
    {
        canMove = true;
    }

}
