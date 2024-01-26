using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rig;

    public PlayerControls playerActions;

    public float moveSpeed;


    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        playerActions = new PlayerControls();
    }

    private void OnEnable()
    {
        playerActions.Enable();
        playerActions.Player.Move.performed += _context => Move(_context);
        playerActions.Player.Move.canceled += _context => StopMove();

    }
    private void OnDisable()
    {
        playerActions.Disable();
        playerActions.Player.Move.performed -= _context => Move(_context);
        playerActions.Player.Move.canceled -= _context => StopMove();


    }

    private void FixedUpdate()
    {
       // rig.velocity = new Vector2( dir.x * moveSpeed, rig.velocity.y);
    }

    void StopMove()
    {
        rig.velocity = new Vector2(0, rig.velocity.y);
    }

    void Move(InputAction.CallbackContext _context)
    {
        Vector2 dir = _context.ReadValue<Vector2>();

        rig.velocity = new Vector2( dir.x * moveSpeed, rig.velocity.y);
    }
}
