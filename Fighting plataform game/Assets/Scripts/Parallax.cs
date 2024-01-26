using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    public float parallaxSpeed;

    private float lenght;
    private float StartPos;

    private Transform cam;


    private void Start()
    {
        cam = Camera.main.transform;
        StartPos = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        float RePos = cam.transform.position.x * (1 - parallaxSpeed);
        float Distance = cam.transform.position.x * parallaxSpeed;

        transform.position = new Vector3(StartPos + Distance, transform.position.y, transform.position.z);

        if(RePos > StartPos + lenght)
        {
            StartPos += lenght;
        }
        else if (RePos < StartPos - lenght)
        {
            StartPos -= lenght;
        }
    }
}
