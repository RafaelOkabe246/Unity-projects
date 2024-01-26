using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public Transform PlayerTransform;

    private Vector3 _cameraOffset;

    [Range(0.01f,1.0f)]
    public float smoothFactor = 0.5f;
    public bool LookAtPlayer = false;
    void Start()
    {
        _cameraOffset = transform.position - PlayerTransform.position;
    }

    
    void LateUpdate()
    {
        Vector3 newPos = PlayerTransform.position + _cameraOffset;

        transform.position = Vector3.Slerp(transform.position,newPos,smoothFactor);

        if(LookAtPlayer)
            transform.LookAt(PlayerTransform);
    }
}
