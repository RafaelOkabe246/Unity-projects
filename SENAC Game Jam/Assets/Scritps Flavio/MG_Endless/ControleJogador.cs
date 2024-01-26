using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleJogador : MonoBehaviour {
    [SerializeField]
    private float speed = 7f, jumpForce = 10f, fallMultiplier = 2.5f;
    private float hInput;
    private bool grounded = true;

    private Rigidbody2D rb;

    public MinijogoController minijogoController;

    private void Start()
    {
        minijogoController = FindObjectOfType<MinijogoController>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        hInput = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            grounded = false;
            Jump(jumpForce);
        }

        if (Input.GetKeyUp(KeyCode.Space) && !grounded)
            rb.velocity = new(rb.velocity.x, 0f);

        if (rb.velocity.y < 0)
        {
            rb.velocity += (fallMultiplier - 1) * Physics2D.gravity.y * Time.deltaTime * Vector2.up;
        }

    }

    private void FixedUpdate()
    {
        MovePlayer(speed);

    }

    private void MovePlayer(float velo)
    {
        rb.velocity = new(hInput * velo, rb.velocity.y);

    }

    private void Jump(float jf)
    {
        rb.velocity = new(rb.velocity.x, jf);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstaculo"))
        {
            //Tocou no objeto, perde o jogo
            minijogoController.MinijogoEnded(false);
        }
    }

}
