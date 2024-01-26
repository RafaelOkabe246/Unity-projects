using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Monster : MonoBehaviour, IDamage
{
    public UiManager uiManager;

    public AudioClip attackClip;

    [SerializeField]  SpriteRenderer zombieSprite;
    [SerializeField]  Animator anim;
    [SerializeField]  Rigidbody2D rig;
    float respawnTime = 10f;
    public float speed;
    public int HP;
    public Transform playerPos;

    public bool canTakeDamage;
    [SerializeField]  bool isAttacking;
    [SerializeField]  bool isDead;
    [SerializeField]  bool canTurn;
    [SerializeField]  bool isTurning;
    [SerializeField]  bool isLookingRight;
    [SerializeField]  bool playerDetected;
    [SerializeField]  bool canMove;
    [SerializeField]  bool isMoving;
    [SerializeField]  float directionMove;
    [SerializeField]  float visionRange;



    public LayerMask playerLayer;

    [SerializeField] Transform attackPoint;

    void Start()
    {
        canMove = true;
    }

    void Update()
    {
        UpdateAnimations();
        if (!isDead && !isAttacking)
            canMove = true;
        else if (isDead)
            canMove = false;

        if(HP <= 0)
        {
            isDead = true;
        }
        else
        {
            isDead = false;
        }
    }

    private void FixedUpdate()
    {
        LookingSight();
        if(canMove)
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

        /*
        Collider2D attackArea = Physics2D.OverlapBox(attackPoint.position, new Vector2(1f, 2f), 0f, playerLayer);
        if (attackArea != null && playerDetected && !isAttacking)
        {
            isAttacking = true;
            anim.SetTrigger("Attack");
        }
        */

        /*
        if (canTurn && !isMoving && !isTurning && !playerDetected)
        {
            isTurning = true;
            StartCoroutine(turning());
        }
        */
    }

    IEnumerator Respawning()
    {
        yield return new WaitForSeconds(respawnTime);
        HP = 3;
        canTakeDamage = true;
        isDead = false;
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
        if (!isDead)
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

        if(canMove)
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

    void Attack()
    {
        Collider2D attackCollider = Physics2D.OverlapBox(attackPoint.position, new Vector2(1f, 2f), 0f, playerLayer);
        if (attackCollider != null)
        {
            attackCollider.GetComponent<IDamage>().TakeDamage(1);
        }
        isAttacking = false;
    }

    void Flip()
    {
        transform.Rotate(0, 180f, 0);
        isLookingRight = !isLookingRight;
    }
    public void KillPlayer()
    {
        StartCoroutine(deathJumpscare());
    }


    IEnumerator deathJumpscare()
    {
        SoundManager.instance.PlaySFX(attackClip);
        uiManager.PlayerDeath();
        yield return new WaitForSeconds(attackClip.length);
        SceneController.instance.PlayAgain();
    }


}
