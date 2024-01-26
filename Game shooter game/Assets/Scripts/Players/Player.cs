using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// As funções básicas do jogador são mover, atirar e pular
    /// 
    /// 1. mover = horizontal
    /// 2. Atirar = Flipar quando a mira estiver no lado oposto
    /// 
    /// </summary>

    [SerializeField] 
    private Rigidbody2D rig;
    public float Speed;
    public Vector2 Direction;
    public bool IsLookLeft;

    public Mira _mira;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }



    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = rig.velocity.y;

        //Flip according to movement
        if(x > 0 && IsLookLeft)
        {
            flip();
        }
        else if(x < 0 && !IsLookLeft)
        {
            flip();
        }

        //Flip according to aim
        float miraX = _mira.Target.x;
        if (miraX > transform.position.x && IsLookLeft)
        {
            flip();
        }
        else if (miraX < transform.position.x && !IsLookLeft)
        {
            flip();
        }
        

        Direction = new Vector2(x,y);
    }

    void FixedUpdate()
    {
        movement(Direction,Speed);
    }

    protected void movement(Vector2 direction, float speed)
    {
        rig.MovePosition((Vector2)rig.transform.position + direction * speed * Time.deltaTime);
    }

    protected void flip()
    {
        IsLookLeft = !IsLookLeft;
        float x = transform.localScale.x * -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }
}
