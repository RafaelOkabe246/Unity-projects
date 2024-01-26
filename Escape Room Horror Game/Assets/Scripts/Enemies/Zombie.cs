using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour, IDamage
{
    [SerializeField] protected SpriteRenderer zombieSprite;
    [SerializeField] protected Animator anim;
    [SerializeField] protected Rigidbody2D rig;
    float respawnTime = 5f;
    public float speed;
    public int HP;
    public Transform playerPos;
    
    public bool canTakeDamage;
    [SerializeField] protected bool isAttacking;
    [SerializeField] protected bool isDead;
    [SerializeField] protected bool canTurn;
    [SerializeField] protected bool isTurning;
    [SerializeField] protected bool isLookingRight;
    [SerializeField] protected bool playerDetected;
    [SerializeField] protected bool canMove;
    [SerializeField] protected bool isMoving;
    [SerializeField] protected float directionMove;
    [SerializeField] protected float visionRange;

    float timeToTurn;


    public LayerMask playerLayer;

    [SerializeField] Transform attackPoint;

    void Start()
    {
        canMove = true;
    }
    
    void Update()
    {
        UpdateAnimations();
    }

    private void FixedUpdate()
    {
        LookingSight();
        if(!isDead && canMove && !isAttacking)
            Movement();
    }

    void LookingSight()
    {
        float dirX;

        if (isLookingRight)
            dirX = 1;
        else
            dirX = -1;

        RaycastHit2D visionCamp = Physics2D.Raycast(transform.position, Vector2.right * dirX, visionRange, playerLayer);
        if (visionCamp.collider != null)
        {
            playerDetected = true;
        }
        else
        {
            playerDetected = false;
        }


        Collider2D attackArea = Physics2D.OverlapBox(attackPoint.position, new Vector2(1f, 2f), 0f, playerLayer);
        if (attackArea != null && playerDetected && !isAttacking)
        {
            isAttacking = true;
            anim.SetTrigger("isAttacking");
        }


        if (canTurn && !isMoving && !isTurning  && !playerDetected)
        {
            isTurning = true;
            StartCoroutine(turning());
        }
    }

    IEnumerator Respawning()
    {
        yield return new WaitForSeconds(respawnTime);
        HP = 2;
        canTakeDamage = true;
        isDead = false;
    }

    IEnumerator turning()
    {
        timeToTurn = Random.Range(3f, 5f);
        yield return new WaitForSeconds(timeToTurn);
        Flip();
        isTurning = false;
    }

    protected virtual void Attack()
    {
        Collider2D attackCollider = Physics2D.OverlapBox(attackPoint.position, new Vector2(1f,2f), 0f, playerLayer);
        if(attackCollider != null)
        {
            attackCollider.GetComponent<IDamage>().TakeDamage(1);
        }
        isAttacking = false;
    }

    public void TakeDamage(int damage)
    {
        //Took damage, player detected
        if (canTakeDamage)
        {
            if (!playerDetected)
            {
                //Flip
                Flip();
            }
            HP -= damage;

            StartCoroutine(DamageEffect());

            if (HP <= 0)
            {
                isDead = true;
                canTakeDamage = false;
                StartCoroutine(Respawning());
            }
        }
    }

    IEnumerator DamageEffect()
    {
        Debug.Log("Effect");
        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(0.1f);
            zombieSprite.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            zombieSprite.color = new Color(1f, 1f, 1f, 1f);
        }

    }

    void Movement()
    {
        if (playerDetected)
        {

            //Change to follow the player
            if (playerPos.transform.position.x > transform.position.x)
            {
                directionMove = 1;
            }
            else if (playerPos.transform.position.x < transform.position.x)
            {
                directionMove = -1;
            }
        }
        else
        {

            directionMove = 0;
        }

        rig.velocity = new Vector2(directionMove * speed, rig.velocity.y);

        if (rig.velocity.x != 0)
        {
            canTurn = false;
            isMoving = true;
        }
        else
        {
            canTurn = true;
            isMoving = false;
        }

        if (isLookingRight && rig.velocity.x < 0)
        {
            Flip();
        }
        else if (!isLookingRight && rig.velocity.x > 0)
        {
            Flip();
        }
    }

    void UpdateAnimations()
    {
        anim.SetBool("Dead", isDead);
        anim.SetBool("isMoving", isMoving);
    }

    void Flip()
    {
        transform.Rotate(0, 180f, 0);
        isLookingRight = !isLookingRight;
    }



}
