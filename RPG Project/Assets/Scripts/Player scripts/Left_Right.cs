using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Left_Right : MonoBehaviour
{
    public GameObject under;
    private Rigidbody2D Rig;
    private Animator PlayerAnimator;

    public float speed;
    private Vector2 moveDirection;

    public bool islookleft;
    public bool isattack;
    public bool isGrounded;
    public bool isClimbing;

    public LayerMask Chão;

    void Start()
    {
        Rig = GetComponent<Rigidbody2D>();
        PlayerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (isGrounded == true)
        {
            moveDirection = new Vector2(moveX, moveY).normalized;
        }


        //Animator variables
        PlayerAnimator.SetInteger("h", (int)moveX);
        PlayerAnimator.SetBool("isGrounded", isGrounded);
        PlayerAnimator.SetBool("isAttack", isattack);
        PlayerAnimator.SetBool("isClimbing", isClimbing);
    }

    void FixedUpdate()
    {
        //Check grounded
        isGrounded = Physics2D.OverlapCircle(under.transform.position, 0.02f, Chão);
        move();
    }

    void move()
    {
        if(moveDirection.x < 0 && !islookleft)
        {
            flip();
        }
        else if (moveDirection.x > 0 && islookleft)
        {
            flip();
        }
        Rig.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
    }

    void flip()
    {
        islookleft = !islookleft;
        float x = transform.localScale.x * -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
        //transform.Rotate(0f, 180f, 0f);
    }
}
