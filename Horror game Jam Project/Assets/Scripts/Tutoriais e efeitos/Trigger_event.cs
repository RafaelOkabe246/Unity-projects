using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Trigger_event : MonoBehaviour
{
    public UnityEvent onTrigger;

    public bool destroyaftertrigger;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            onTrigger.Invoke();

        }

        if (destroyaftertrigger)
        {
            Destroy(this.gameObject, 10f);
        }
    }
}
