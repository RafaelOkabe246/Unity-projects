using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PontoLuz : MonoBehaviour
{
    public float radius = 5f;

    private void Update()
    {
        //DetectShadowArea();
    }

    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Shadow_Area")
        {
            other.gameObject.GetComponent<ShadowArea>().IsOnLight = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag =="Shadow_Area")
        {
            other.gameObject.GetComponent<ShadowArea>().IsOnLight = false;
        }
    }
    */
}
