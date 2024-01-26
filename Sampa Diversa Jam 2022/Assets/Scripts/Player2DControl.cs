using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player2DControl : MonoBehaviour
{
    public float MovementSpeed;
    Rigidbody2D rig;
    public Camera CameraMain;
    internal Vector3 Target;

    [Header("Luz e sombras")]
    public int LuzesSombras;
    public Score score;
    public LayerMask Plataform;
    public BGEffects _BGEffects;

    //Animação Personagem
    private bool olhaEsquerda = true;
    private bool olhaDireita;
    private bool olhaFrente = true;
    private bool olhaCostas;
    public Animator animator;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _BGEffects = GameObject.FindGameObjectWithTag("BG_effects").GetComponent<BGEffects>();
        score = GetComponent<Score>();   
    }

    private void Update()
    {
        Target = CameraMain.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
    }


    private void FixedUpdate()
    {
        //movement
        Vector2 currentPos = rig.position;
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
   
        Movement(inputVector);

        if (inputVector != Vector2.zero)
        {

            _BGEffects.CheckShadowLight(LuzesSombras);
        }

        //animation Sprite Player
        if (horizontalInput < 0)
        {
            olhaEsquerda = true;
            olhaDireita = false;
        }
        if (horizontalInput > 0)
        {
            olhaEsquerda = false;
            olhaDireita = true;
        }
        if (verticalInput < 0)
        {
            olhaFrente = true;
            olhaCostas = false;
        }
        if (verticalInput > 0)
        {
            olhaFrente = false;
            olhaCostas = true;
        }

        animator.SetBool("olhaEsquerda", olhaEsquerda);
        animator.SetBool("olhaDireita", olhaDireita);
        animator.SetBool("olhaFrente", olhaFrente);
        animator.SetBool("olhaCostas", olhaCostas);

    }

    void Movement(Vector2 direction)
    {
        rig.MovePosition((Vector2)transform.position + (direction * MovementSpeed * Time.fixedDeltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == Plataform)
        {
            collision.gameObject.GetComponent<InteractiveBlock>().Activating();
        }  
        if (collision.gameObject.tag == "Luz")
        {
            LuzesSombras = LuzesSombras + 1;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Sombra")
        {
            LuzesSombras = LuzesSombras - 1;
            Destroy(collision.gameObject);
        }
    }

}
