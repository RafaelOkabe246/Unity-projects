using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
    Shooting,
    Dialogue,
    Radio
}

public class Player : MonoBehaviour, IDamage
{
    [SerializeField] SpriteRenderer playerSprite;
    [SerializeField] Rigidbody2D rig;
    public PlayerStats playerStats;
    public Animator anim;
    public IInteractive interactiveObject;
    public bool hasKey;

    [Header("shooter mode")]
    public GameObject aim;
    public GameObject bulletPreFab;
    public GameMode _GameMode;
    public Transform shootSpawn;
    public LayerMask monsterLayer;
    public AudioClip shootingClip;
    public AudioClip reloadClip;

    float shootDirX;
    [SerializeField] float dirX;
    [SerializeField] float speedWalk;
    [SerializeField] bool isLookingRight;
    [SerializeField] bool isWalking;

    bool canWalk;
    bool isReloading;
    public bool isHiding;

    private void Start()
    {
        playerStats.isAlive = true;
    }
    private void Update()
    {
        CheckInput();

        if(isLookingRight && rig.velocity.x < 0)
        {
            Flip();
        }
        else if (!isLookingRight && rig.velocity.x > 0)
        {
            Flip();
        }

        if (isLookingRight)
        {
            shootDirX = 1f;
        }
        else
        {
            shootDirX = -1f;
        }

        if (_GameMode != GameMode.Shooting)
        {
            rig.velocity = new Vector2(0, rig.velocity.y);
            canWalk = false;
        }
        else
            canWalk = true;


        if (isHiding)
        {
            this.gameObject.layer = 0;
            playerSprite.color = new Color(1f, 1f, 1f, 0.4f);
        }
        else
        {
            this.gameObject.layer = 3;
            playerSprite.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    private void FixedUpdate()
    {
        if(playerStats.isAlive && canWalk && !isHiding)
            Movement();
    }

    void Flip()
    {
        transform.Rotate(0, 180f, 0);
        isLookingRight = !isLookingRight;
    }

    void Movement()
    {
        rig.velocity = new Vector2(dirX * speedWalk , rig.velocity.y);
    }

    void CheckInput()
    {
        anim.SetBool("isWalking", isWalking);

        dirX = Input.GetAxisRaw("Horizontal");

        if(dirX != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            isReloading = true;
            //Reload ammo
            if (isReloading)
            {
                isReloading = false; 
                anim.SetTrigger("Reload");
            }
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            //Try to interact
            if(_GameMode != GameMode.Dialogue)
                Interact();
        }
        if (_GameMode == GameMode.Shooting)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Atirar
                Shoot();
            }
        }


    }

    void Reload()
    {
        SoundManager.instance.PlaySFX(reloadClip);
        playerStats.ammunitionNumber = 6;
    }

    void Interact()
    {
        interactiveObject.PlayEvent(this);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Interactive"))
        {
            interactiveObject = other.GetComponent<interactiveObject>();
        }        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Interactive"))
        {
            interactiveObject = null;
        }
    }


    public void TakeKey()
    {
        hasKey = true;
    }

    private void Shoot()
    {
        //Atirar a munição
        if (playerStats.ammunitionNumber > 0)
        {
            //Start shooting animation
            //Shooting();
            anim.SetTrigger("Shoot");
        }
        else if (playerStats.ammunitionNumber <= 0)
        {
            Debug.Log("no ammunition");
        }
    }

    private void Shooting()
    {
        SoundManager.instance.PlaySFX(shootingClip);
        playerStats.ammunitionNumber -= 1;
        RaycastHit2D[] hasFindMonster = Physics2D.RaycastAll(shootSpawn.position, new Vector2(shootDirX, transform.position.y), 20f);

        foreach (RaycastHit2D _object in hasFindMonster)
        {
            if (_object.collider.CompareTag("Monster"))
            {
                _object.collider.gameObject.GetComponent<IDamage>().TakeDamage(1);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Tomou dano");
       // playerStats.isAlive = false;
    }

    public void HideShow()
    {
        isHiding = !isHiding;
        
    }

    public void ChangeGameMode(int i)
    {
        switch (i)
        {
            case (0):
                _GameMode = GameMode.Shooting;
                break;
            case (1):
                _GameMode = GameMode.Radio;
                break;
            case (2):
                _GameMode = GameMode.Dialogue;
                break;
        }
    }

}
