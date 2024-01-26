using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Boss_Patterns : MonoBehaviour
{
    private Rigidbody2D Rig;

    public Girl _Girl;

    [Header("Jumping variables")]
    public float jumpForce;
    public float jumpHeight;

    public Transform groundCheck;

    public bool isGrounded;

    public LayerMask Ground;

    [Header("Dashing variables")]
    public float speed;


    void Start()
    {
        Rig = this.GetComponent<Rigidbody2D>();   
    }

    
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, Ground);

        //Jumping
        
    }

    public void Dashing()
    {

    }

    public void Jumping()
    {
        float distance_to_player = _Girl.gameObject.transform.position.x - transform.position.x;
        if (isGrounded)
        {
            Rig.velocity = new Vector2();
        }
    }
}
