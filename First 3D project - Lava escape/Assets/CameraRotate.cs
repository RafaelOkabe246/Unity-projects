using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public float turnSpeed = 50f;
    [SerializeField] internal Player player;

    public Vector3 rotation;
    public float x;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //x = 1;
            //player.transform.Rotate(0f, -turnSpeed * Time.deltaTime, 0f);
            //transform.Rotate(0f, turnSpeed * Time.deltaTime, 0f);
            //transform.eulerAngles = new Vector3(0f, -turnSpeed * Time.deltaTime, 0f);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //x = -1;
            //player.transform.Rotate(0f, turnSpeed * Time.deltaTime, 0f);
            //transform.Rotate(0f, -turnSpeed * Time.deltaTime, 0f);
        }


    }

    private void FixedUpdate()
    {
        
    }

}
