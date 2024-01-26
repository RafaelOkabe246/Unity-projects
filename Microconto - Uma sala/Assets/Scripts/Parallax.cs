using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float lenght, startPos;
    public GameObject Cam;
    public float ParallaxEffect;

    void Start()
    {
        startPos = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
        Cam = Camera.main.gameObject;
    }


    void Update()
    {
        //All relative to camera

        float temp = (Cam.transform.position.x * (1 - ParallaxEffect));
        float dist = (Cam.transform.position.x * ParallaxEffect);

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        if (temp > startPos + lenght) startPos += lenght;
        else if (temp < startPos - lenght) startPos -= lenght;
    }
}
