using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] internal Player _character;

    private void Awake()
    {
        _character = GetComponent<Player>();
    }

    public void Movement(Vector2 direction, float speed)
    {
        _character.rig.velocity = new Vector2(direction.x * speed, _character.rig.velocity.y);
        if (direction.x < 0 && _character.IsLookLeft == false)
        {
            flip();
        }
        else if (direction.x > 0 && _character.IsLookLeft == true)
        {
            flip();
        }
    }
    void flip()
    {
        _character.IsLookLeft = !_character.IsLookLeft;
        float x = transform.localScale.x * -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

    public void Jump (Vector2 direction, float JumpForce)
    {
        _character.rig.velocity = direction * JumpForce;
    }

    private void FixedUpdate()
    {
        Movement(_character.direction, _character.speed);
        if (_character.airControlTimer > _character.aircontrol && _character.isGrounded == false)
        {
            if (_character.speed > _character.MinSpeed)
            {
               // _character.speed -= Time.deltaTime;
            }
        }
    }

    private void Update()
    {
        _character.Xspeed = Input.GetAxisRaw("Horizontal");

        //Jumping control
        if (Input.GetKeyDown(KeyCode.Space) && _character.isGrounded == true)
        {
            _character.isJumping = true;
            _character.jumpTimeCounter = _character.jumpTime;
            Jump(Vector2.up, _character.JumpForce);
        }
        if (Input.GetKey(KeyCode.Space) && _character.isJumping == true)
        {
            if(_character.jumpTimeCounter > 0)
            {
                Jump(Vector2.up, _character.JumpForce);
                _character.jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                _character.isJumping = false;
            }

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _character.isJumping = false;
        }


        //Air control
        if(_character.isGrounded == true)
        {
            _character.airControlTimer = 0;
            _character.speed = _character.MaxSpeed;
        }
        else
        {
            if (_character.airControlTimer < _character.aircontrol)
            {
                _character.airControlTimer += Time.deltaTime;
            }
        }
    }
}
