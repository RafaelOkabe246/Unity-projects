using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Boss : MonoBehaviour
{
    public float speed = 20f;
    private Rigidbody2D Rig;

    private Boss _Boss;

    void Start()
    {
        _Boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>();
        Rig = this.GetComponent<Rigidbody2D>();
        Rig.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
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
