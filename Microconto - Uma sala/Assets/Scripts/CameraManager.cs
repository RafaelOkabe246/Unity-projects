using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform[] CameraPos;
    public int actualPos;

    public Camera cam;
    private void Start()
    {
        cam = Camera.main;
        actualPos = 0;
        cam.transform.position = CameraPos[actualPos].transform.position;
    }

    private void Update()
    {
        //cam.transform.position = CameraPos[actualPos].transform.position;


    }

    public void nextPos()
    {
        if (actualPos < CameraPos.Length)
        {
            actualPos += 1;
            cam.transform.position = CameraPos[actualPos].transform.position;
        }

    }

    public void backPos()
    {
        if (actualPos > 0)
        {
            actualPos -= 1;
            cam.transform.position = CameraPos[actualPos].transform.position;
        }
    }
}
