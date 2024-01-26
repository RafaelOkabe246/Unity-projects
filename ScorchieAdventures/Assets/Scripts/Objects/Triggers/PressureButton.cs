using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Can activate and deactivate the objects triggers
public class PressureButton : TriggerObject
{
    [SerializeField] protected Transform detectPoint;
    [SerializeField] protected bool isPressed;

    public override void TriggerEvent()
    {
        Event();
    }

    private void Update()
    {
        PressedCheck();
    }

    protected virtual void PressedCheck()
    {
        Collider2D check = Physics2D.OverlapCircle(detectPoint.position, 0.55f);
        isPressed = check;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isPressed || collision.gameObject.CompareTag("Obstacle") && isPressed)
        {
            OnPressed();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnRelease();
        }
    }

    protected virtual void OnPressed()
    {
        objAnim.SetBool("isActivate", true);
    }

    protected virtual void OnRelease()
    {
        objAnim.SetBool("isActivate", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(detectPoint.position, 0.55f);
    }
}
