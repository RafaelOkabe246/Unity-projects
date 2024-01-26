using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public enum Estados { Atacando, Idle, Correndo, pulando}

public class Boss : MonoBehaviour
{
    [Header("Battle")]
    public float vidas = 100f;
    public float damage;
    private Rigidbody2D Rig;
    public GameObject Gameobject_healthbar;
    public HealthBar healthbar;


    [Header("Jumping")]
    [SerializeField] float JumpHeight;
    public Transform groundCheck;
    [SerializeField] Vector2 boxSize;
    public bool isGrounded;
    public LayerMask Plataform;

    [Header("Dano")]
    private SpriteRenderer SR;
    private Animator Anim;
    public Color HitColor;
    public Color NormalColor;

    public Transform shoot_point;
    public GameObject Shoot;

    [Header("Controle de estados")]
    public Estados Estado_atual;


    [Header("Animator variables")]
    public bool Defeated;
    public bool isFighting;
    public bool CanChangeState;


    [Header("Direction variables")]
    public bool islookleft;
    public Transform Player;
    public Vector2 Player_vector;
    public Vector2 dir;

    [Header("Cutscenes")]
    public Boss_manager _Boss_manager;

    void Start()
    {
        Rig = this.GetComponent<Rigidbody2D>();
        Anim = this.GetComponent<Animator>();
        isFighting = false;
        SR = this.GetComponent<SpriteRenderer>();
        healthbar.gameObject.SetActive(false);
        Gameobject_healthbar.SetActive(false);
    }


    void Update()
    {
        //Vida
        healthbar.health = vidas;
        healthbar.maxHealth = 100f;

        if(vidas <= 0)
        {
            Defeated = true;
            Anim.SetBool("Defeated", Defeated);
            isFighting = true;
            Lost_fight();
            //Lose.Invoke();
        }

        if(isFighting == true)
        {
            Gameobject_healthbar.SetActive(true);
            healthbar.gameObject.SetActive(true);
            Anim.SetTrigger("isFighting");
            Start_Battle();
            isFighting = false;
        }

        Player_vector = new Vector2(Player.position.x, Player.position.y);
        
        dir = new Vector2(transform.position.x, transform.position.y);

        Horizontal_Direction();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, Plataform); 
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shot"))
        {
            vidas += -10;
            StartCoroutine("DamageController");
        }
    }


    private void Horizontal_Direction()
    {

        //Turn left and right
        if (dir.x < Player_vector.x && islookleft == true)
        {
            //enemy is in the right
            islookleft = false;
            flip();
        }
        else if(dir.x > Player_vector.x && islookleft == false)
        {
            //enemy is in the left
            islookleft = true;
            flip();
        }

    }

    void flip()
    {
        float x = transform.localScale.x * -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
        //transform.Rotate(0f, 180f, 0f);
    }

    IEnumerator DamageController()
    {
        SR.color = HitColor;
        yield return new WaitForSeconds(0.2f);
        SR.color = NormalColor;
        yield return new WaitForSeconds(0.2f);
        SR.color = HitColor;
        yield return new WaitForSeconds(0.2f);
        SR.color = NormalColor;
        yield return new WaitForSeconds(0.2f);

    }



    void Shooting()
    {
        Instantiate(Shoot, shoot_point.position, shoot_point.rotation);
    }

    public void Start_Battle()
    {
        isFighting = true;
    }

    //Events
    public void Lost_fight()
    {
        _Boss_manager.Boss_defeated();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(groundCheck.position, boxSize);
    }
}
