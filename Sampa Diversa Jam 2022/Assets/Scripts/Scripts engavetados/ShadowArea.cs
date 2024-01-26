using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowArea : MonoBehaviour
{
    public bool IsOnLight;


    private void Update()
    {
        CheckLight();
    }

    public void CheckLight() 
    {
        if(IsOnLight == true)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        else 
        { 
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Luz")
        {
            IsOnLight = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Luz")
        {
            IsOnLight = false;
        }
    }

}
