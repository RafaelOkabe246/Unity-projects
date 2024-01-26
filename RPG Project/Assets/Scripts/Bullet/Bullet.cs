using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    private Rigidbody2D Rig;

    void Start()
    {
        Rig = GetComponent<Rigidbody2D>();
        Rig.velocity = transform.right * speed;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boss") || collision.gameObject.CompareTag("Plataforma") || collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
        else
        {

        }
    }

    private void Update()
    {
        Destroy(gameObject, 1f);
    }

}
