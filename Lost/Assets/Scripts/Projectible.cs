using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectible : MonoBehaviour
{
    public float speed;

    private bool isShooting;

    private Transform player;
    private Transform player_lastposition;

    private Vector2 Target;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        Target = new Vector2(player_lastposition.position.x, player_lastposition.position.y);
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Target, speed * Time.deltaTime);

        if (transform.position.x == Target.x && transform.position.y == Target.y)
        {
            Projectile_Destroy();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DestroyProjectile();
        }
    }


    void Projectile_Destroy()
    {
        Destroy(this.gameObject, 3f);
    } 

    void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }


}
