using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{ 
    public float speed_rotation;
    private GameController _GameController;


    void Start()
    {
        speed_rotation = 30f;
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
    }

    void Update()
    {
        if (_GameController.currentTime < 95f)
        {
            transform.Rotate(Vector3.forward, Time.deltaTime * speed_rotation);
        }

        if (_GameController.currentTime > 95f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, 1f * Time.deltaTime);
        }
    }

}
