using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public float JumpForce;
    private float jumpTimeCounter;
    public float JumpTime;
    [SerializeField]
    private bool isJumping;

    public bool isLookLeft;
    public float Speed;
    public float moveInput;

    [SerializeField]
    private Rigidbody rig;

    public bool IsGrounded;
    public Transform GroundCheck;
    public LayerMask WhatIsGround;

    public ObjectsManager _ObjectsManager;

    private Animator _animator;

    //Steps sounds

    public SoundManager _SoundManager;

    private void Start()
    {
        _SoundManager = FindObjectOfType(typeof(SoundManager)) as SoundManager;
        _animator = this.GetComponent<Animator>();
        rig = this.GetComponent<Rigidbody>();
        //_ObjectsManager = GameObject.FindGameObjectWithTag(Tags.ObjectManager).GetComponent<ObjectsManager>();
        _SoundManager.AudioPlay(_SoundManager.CompleteLevel,0.5f);
    }

    private void Update()
    {
        if (Physics.CheckSphere(GroundCheck.position, 1f, WhatIsGround))
        {
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
        }


        if (Input.GetKey(KeyCode.Q))
        {
          //  _ObjectsManager.ChangeColorBlack();
        }
        if (Input.GetKey(KeyCode.E))
        {
        //    _ObjectsManager.ChangeColorWhite();
        }


        if (IsGrounded == true && Input.GetKeyDown(KeyCode.W) || IsGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = JumpTime;
            Jump();
        }

        if (Input.GetKey(KeyCode.W) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                Jump();
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            isJumping = false;
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            SceneController.ResetLevel();
        }

        if (moveInput != 0)
        {
            _animator.SetBool("isMoving", true);
        }
        else
        {
            _animator.SetBool("isMoving", false);
        }

    }

    private void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        if (moveInput > 0 && isLookLeft)
        {
            //1
            flip();
        }
        else if(moveInput < 0 && !isLookLeft)
        {
            //-1
            flip();
        }

        MovimentoHorizontal();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tags.Finish))
        {
            other.gameObject.GetComponent<FinalPoint>().NextPhase();
        }
        if (other.gameObject.CompareTag(Tags.Death))
        {
            //Die
            SceneController.ResetLevel();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Tags.Death))
        {
            //Die
            SceneController.ResetLevel();
        }
    }

    public void AudioSteps()
    {
        if (IsGrounded == true)
        {
            _SoundManager.AudioPlay(_SoundManager.PlayerSteps, 1f);
        }
    }

    void flip()
    {
        isLookLeft = !isLookLeft;
        float x = transform.localScale.x * -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
        //transform.Rotate(0f, 180f, 0f);
    }

    void MovimentoHorizontal()
    {

            rig.velocity = new Vector3(moveInput * Speed, rig.velocity.y, transform.position.z);

    }

    void Jump()
    {
        rig.velocity = Vector2.up * JumpForce;
    }
}
