using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Bird : MonoBehaviour
{
    public float jumpForce;
    private Rigidbody2D rig;

    public PlayerInput playerInput;
    public InputAction tap;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rig = GetComponent<Rigidbody2D>();
        tap = playerInput.actions["Tap"];

    }

    private void OnEnable()
    {
        tap.performed += Jump;
    }

    private void OnDisable()
    {
        tap.performed -= Jump;

    }

    private void Update()
    {
        
        if (Mouse.current.leftButton.wasPressedThisFrame && GameManager.instance.currentState == GameState.Gameplay && !isMouseOverUi() )
        {
            rig.velocity = Vector2.up * jumpForce;
            SoundManager.instance.PlaySound(SoundManager.instance.flapClip);
        }
        
        if (GameManager.instance.currentState == GameState.Lose)
        {
            gameObject.SetActive(false);
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("GQ");
        if (GameManager.instance.currentState == GameState.Gameplay)
        {
            rig.velocity = Vector2.up * jumpForce;
            SoundManager.instance.PlaySound(SoundManager.instance.flapClip);
        }
    }

    private bool isMouseOverUi()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0,0, rig.velocity.y * 5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.instance.ChangeState(GameState.Lose);
        SoundManager.instance.PlaySound(SoundManager.instance.dieClip);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PointWall"))
        {
            GameManager.instance.AddPoints();
        }
    }


}
