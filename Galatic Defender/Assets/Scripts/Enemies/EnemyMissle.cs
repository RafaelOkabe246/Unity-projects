using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissle : Enemy
{
    public Transform target;
    public float speed = 10f;
    public float explosionRadius;


    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 0.01f)
        {
            //Hit the target, play the explosion animation
            _animator.SetTrigger("Explodiu");
            
        }

    }

    public void ExplosionArea()
    {
        Collider2D[] objectsInRange = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D obj in objectsInRange)
        {
            if (obj.CompareTag("Building"))
            {
                //Get the script and decrease life
                obj.GetComponent<Building>().TakeDamage(1);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
