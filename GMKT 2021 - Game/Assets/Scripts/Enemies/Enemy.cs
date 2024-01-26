using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float MaxHealth;
    [SerializeField] 
    protected float CurrentHealth;

    private Rigidbody2D rig;

    private Animator animator;

    public float MoveSpeed;
    [SerializeField] 
    protected bool isLookLeft;
    protected Vector2 direction;

    public Transform AttackArea;
    public LayerMask Player;
    public int AttackDamage;

    [Header("Patrol")]
    public LayerMask Plataform;
    public bool IsPlayer_in_Range;
    public Transform Ground_check;
    public float View_area = 10f;

    [Header("Animator")]
    protected Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        CurrentHealth = MaxHealth;
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Attack()
    {
        //Set the animation
        animator.SetTrigger("Attacking");

        //Detect the enemies
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(AttackArea.position, 0.5f, Player);

        //Do the damage
        foreach (Collider2D Player in hitPlayer)
        {
            Player.GetComponent<Player>().TakeDamage(AttackDamage);
        }
    }

    protected virtual void Movement(Vector2 direction)
    {
        rig.MovePosition((Vector2)transform.position + direction * MoveSpeed * Time.deltaTime);
        if (direction.x < 0 && isLookLeft == false)
        {
            flip();
        }
        else if (direction.x > 0 && isLookLeft == true)
        {
            flip();
        }
    }
    protected virtual void flip()
    {
        isLookLeft = !isLookLeft;
        float x = transform.localScale.x * -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }
    public void TakeDamage(int damage_recived)
    {
        CurrentHealth -= damage_recived;

        _animator.SetTrigger("Take damage");

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void trajetoria()
    {
        //Trajetória
        if (isLookLeft == true)
        {
            direction = Vector2.left;
        }
        else
        {
            direction = Vector2.right;
        }
    }


    protected virtual void Die()
    {
        Destroy(this.gameObject);
    }

}
