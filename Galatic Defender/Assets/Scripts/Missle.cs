using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{
    public Transform target;
    public float speed = 10f;
    public float explosionRadius;

    [SerializeField] private Animator animator;

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        
        if(Vector2.Distance(transform.position, target.position) < 0.01f)
        {
            //Hit the target, play the explosion animation
            animator.SetTrigger("Explodiu");
        }
        
    }

    public void ExplosionArea()
    {
        Collider2D[] objectsInRange = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach(Collider2D obj in objectsInRange)
        {
            if (obj.CompareTag("Enemy"))
            {
                //Get the script and destroy the object
                Destroy(obj.gameObject);
            }
        }
    }

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
