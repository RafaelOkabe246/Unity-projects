using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Elevador : MonoBehaviour
{
    [SerializeField] private GameObject Ponte;

    [SerializeField] private bool isPressed;

    [SerializeField] internal Transform Initial_pos;
    [SerializeField] internal Transform Final_pos;


    private void Update()
    {
        //isPressed = false;
        if (isPressed == true)
        {
            Mover(Final_pos);
        }
        else if (isPressed == false)
        {
            Mover(Initial_pos);
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isPressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isPressed = false;
        }
    }


    void Mover(Transform pos)
    {
        Ponte.gameObject.transform.position = Vector2.Lerp(Ponte.gameObject.transform.position, pos.position,  Time.deltaTime); ;
    }
}
