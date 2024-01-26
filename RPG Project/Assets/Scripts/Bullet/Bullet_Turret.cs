using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Turret : MonoBehaviour
{


    void Start()
    {

        transform.Rotate(0f, 180f, 0f);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
   
    }

    private void Update()
    {
        Destroy(gameObject, 1f);
    }
}
