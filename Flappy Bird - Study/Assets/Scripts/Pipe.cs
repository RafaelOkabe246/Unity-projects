using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public float moveSpeed;

    private void FixedUpdate()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.fixedDeltaTime);
    }
}
