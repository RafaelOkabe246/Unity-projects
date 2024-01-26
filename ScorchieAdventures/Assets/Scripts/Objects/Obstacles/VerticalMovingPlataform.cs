using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovingPlataform : Obstacle
{
    public LayerMask playerLayer;
    [SerializeField] private float verticalSpeed;
    private bool isPressed;
    private bool isColliding;
    [SerializeField] private bool canMove;
    [SerializeField] private Vector2 detectionSize;
    public Transform detectPoint;
    public Transform detectPointUp;

    public override void Event()
    {
        if (isPressed && !isColliding)
        {
            rig.velocity = Vector2.up * verticalSpeed;
        }
    }

    private void Update()
    {
        CollisionCheck();
    }
    void CollisionCheck()
    {
        isPressed = Physics2D.OverlapBox(detectPoint.position, detectionSize, 0, playerLayer);
        objAnim.SetBool("isPressed", isPressed);

        if (!isPressed)
            return;

        isColliding = Physics2D.OverlapCapsule(detectPointUp.position, new Vector2(5f, 1f), CapsuleDirection2D.Horizontal, 0f, groundLayer);
    }

    private void SetMove()
    {
        canMove = true;
    }

    private void CancelMove()
    {
        canMove = false;
    }

    private void FixedUpdate()
    {
        Event();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(detectPoint.position, detectionSize);
    }

}
