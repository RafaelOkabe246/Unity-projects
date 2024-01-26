using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movement : MonoBehaviour
{
    [SerializeField] internal Player _Player;

    [SerializeField] internal bool isGrounded;
    [SerializeField] internal bool islookleft;
    [SerializeField] internal bool isClimbing;
    [SerializeField] internal bool isInLadder;

    public float distance;

    [SerializeField] internal bool canDie;

    public LayerMask Joana = 11;
    
    private void FixedUpdate()
    {
        //=============
        //Check ground 
        //=============

        RaycastHit2D groundInfo = Physics2D.Raycast(_Player.groundCheckPosition.position, Vector2.down, 0.2f);
        isGrounded = groundInfo;

        if (_Player.Estado_atual == Modos.Ativo)
        {
            Movement(_Player.Horizontal_direction);
        }


        //================
        //Ladder movement
        //================
        RaycastHit2D ladderInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, _Player.Ladder);
        isInLadder = ladderInfo;

        if(ladderInfo.collider != null)
        {
            if (_Player.Inputs.upIsPressed == true)
            {
                isClimbing = true;
            }
        }
        else
        {
            isClimbing = false;
        }

        if(isClimbing == true)
        {
            LadderMovement(_Player.Vertical_direction);
        }

        //==========================
        //Fall death
        //==========================
        float FallVelocity = _Player.Rig.velocity.y;

        if (isGrounded == false)
        {
           if(FallVelocity < -15f)
           {
                canDie = true;
           }
           else if (FallVelocity > -15f)
           {
                canDie = false;
           }

        }
        else if(canDie == true && isGrounded == true)
        {
            canDie = false;
            _Player.Rig.velocity = new Vector2(_Player.Rig.velocity.x, 0);
            _Player.transform.position = _Player.Respawn.position;
        }
        
    }

    void LadderMovement(float InputVertical)
    {
        _Player.Rig.velocity = new Vector2(_Player.Rig.velocity.x, InputVertical * 5f);
    }

    void Movement(Vector2 direction)
    {

            _Player.Rig.velocity = new Vector2(direction.x * _Player.speed * Time.deltaTime, _Player.Rig.velocity.y);

            if (direction.x > 0 && islookleft == true)
            {
                flip();
            }
            else if (direction.x < 0 && islookleft == false)
            {
                flip();
            }

    }



    public void JumpForce()
    {
        _Player.Rig.AddForce(Vector2.up * _Player.JumpForce);
    }

    void flip()
    {
        islookleft = !islookleft;
        float x = transform.localScale.x * -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
        //transform.Rotate(0f, 180f, 0f);
    }
}
