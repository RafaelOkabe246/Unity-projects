using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ativador : MonoBehaviour
{
    [SerializeField] private GameObject Ponte;


    [SerializeField] private float Min_Ponte_size = 1f;
    [SerializeField] internal float Max_Ponte_size;



    [SerializeField] private bool isPressed;

    private void Update()
    {
        //isPressed = false;
        if (isPressed == true)
        {
            Esticando_a_ponte(Max_Ponte_size);
        }
        else if(isPressed == false)
        {
            Esticando_a_ponte(Min_Ponte_size);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8)
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

    void Esticando_a_ponte(float maxSize)
    {
        Ponte.transform.localScale = new Vector3(Mathf.Lerp(Ponte.transform.localScale.x, maxSize, Time.deltaTime), Ponte.transform.localScale.y, Ponte.transform.localScale.z);
    }
}
