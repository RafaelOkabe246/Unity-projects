using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySoldier : MonoBehaviour
{
    public enum Estados { Atacando, Mocvendo }
    public Estados Estado_atual;
    public LayerMask Base;

    public float speed;
    private Base Barricade;
    private Rigidbody2D rig;
    public Vector2 movement_direction;

    private bool Reach_the_base;

    private Animator _Animator;

    void Awake()
    {
        Barricade = GameObject.FindGameObjectWithTag("Base").GetComponent<Base>();
        rig = this.GetComponent<Rigidbody2D>();
        _Animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement_direction = new Vector2(0, -1);

        Reach_the_base = Physics2D.Raycast(transform.position, Vector2.down, 2f, Base);

        CheckState();
    }

    void FixedUpdate()
    {
        Movement(movement_direction);
    }   

    void Movement(Vector2 direction)
    {
        rig.velocity = movement_direction * speed;
        
    }

    void CheckState()
    {
        if(Reach_the_base == true)
        {
            Estado_atual = Estados.Atacando;
        }
        else
        {
            Estado_atual = Estados.Mocvendo;
        }

        switch (Estado_atual)
        {
            case Estados.Atacando:
                speed = 0;
                _Animator.SetBool("isShooting", true);
                break;
            
            case Estados.Mocvendo:
                
                break;
        }
            
    }


    public void Attacking()
    {
        Barricade.Lifes -= 1;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(this.gameObject);
        }
    }
}
