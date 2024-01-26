using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_drone : MonoBehaviour
{
    public float speed = 20f;
    private Rigidbody2D Rig;

    void Start()
    {
        Rig = GetComponent<Rigidbody2D>();
        transform.Rotate(0f, 180f, 0f);
        Rig.velocity = transform.right * speed;

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
