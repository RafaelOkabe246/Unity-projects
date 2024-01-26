using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rig;
    public float Speed, JumpForce;
    private Vector2 direction;
    public bool IsLookLeft;

    public PlayerActionsControl playerActionsControl;

    void Awake()
    {
        rig = GetComponent<Rigidbody2D>();

        if (playerActionsControl == null)
        {
            playerActionsControl = new PlayerActionsControl();
        }
        //Add the functions
        playerActionsControl.Player.Shoot.performed += add_that_function_to_this_list => Shoot();
        playerActionsControl.Player.Move.performed += context => direction = context.ReadValue<Vector2>();

    }

    //You need to enable your controls
    void OnEnable()
    {
        playerActionsControl.Enable();

    }

    private void OnDisable()
    {
        playerActionsControl.Disable();
    }

    private void Update()
    {
        float x = playerActionsControl.Player.HorizonalVector.ReadValue<float>();

        //Flip according to movement
        if (x > 0 && IsLookLeft)
        {
            flip();
        }
        else if (x < 0 && !IsLookLeft)
        {
            flip();
        }
    }

    private void FixedUpdate()
    {
        Movement(direction, Speed);
    }

    void Shoot()
    {
        Debug.Log("Test shoot");
    }

    void Movement(Vector2 direction, float speed)
    {
        //Debug.Log(direction);
        rig.MovePosition((Vector2)transform.position + direction * speed * Time.deltaTime);
    }

    void flip()
    {
        IsLookLeft = !IsLookLeft;
        float x = transform.localScale.x * -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

}
