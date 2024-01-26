using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //[SerializeField] internal Animator _animator;
    [SerializeField] internal PlayerAnimations _animations;
    [SerializeField] internal PlayerInputs inputs;
    [SerializeField] internal PlayerMovement _movement;
    [SerializeField] internal PlayerCollider _PlayerSollider;
    [SerializeField] internal Rigidbody2D rig;
    [SerializeField] internal Collider2D _collider;

    [Header("Special effects")]
    public CameraShake _CameraShake;

    [Header("Respawn atributes")]
    public Transform Checkpoint;

    //Movement atributes
    public float MaxSpeed;
    public float MinSpeed;
    public float speed;
    public Vector2 direction;

    public bool IsLookLeft;

    public float Xspeed;
    public float Yspeed;

    public bool isRunning;

    //Jump
    public float JumpForce;
    public Transform GroundCheck;
    public bool isGrounded;

    public float airControlTimer;
    public float aircontrol;

    public bool isJumping;

    public float jumpTimeCounter;
    public float jumpTime;

    //Attack atributes
    public bool isAttacking;
    public Transform AttackArea;
    public int AttackDamage = 3;

    public GameObject particle;

    public LayerMask Enemy;

    public float AttackRate = 2f;
    internal float nextAttackTime = 0f;

    //Life atributes
    [SerializeField] float CurrentHealth;
    public float MaxHealth = 10f;

    void Awake()
    {
        CurrentHealth = MaxHealth;
        inputs = GetComponent<PlayerInputs>();
        rig = GetComponent<Rigidbody2D>();
        _movement = GetComponent<PlayerMovement>();
        _collider = GetComponent<Collider2D>();
    }


    private void FixedUpdate()
    {
        direction.x = Xspeed;

        //Check ground
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, 0.1f);
    }


    //Powers attack body-to-body 
    public void Attack()
    {
        //Set the animation
        //_animations._animator.SetTrigger("Attacking");

        //Detect the enemies
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackArea.position, 0.5f, Enemy);

        //Do the damage
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(AttackDamage);
        }

        //Do the special effect
        StartCoroutine( _CameraShake.Shake(.15f, 0.4f));
        GameObject a=  Instantiate(particle, AttackArea.position, Quaternion.identity) as GameObject;
        Destroy(a.gameObject,0.5f);
    }

    public void TakeDamage(int damage_recived)
    {

        CurrentHealth -= damage_recived;

        if (CurrentHealth <= 0)
        {
            
        }
    }

}
