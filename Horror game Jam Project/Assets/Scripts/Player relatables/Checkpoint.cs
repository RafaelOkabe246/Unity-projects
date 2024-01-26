using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Checkpoint : MonoBehaviour
{
    private Checkpoint_manager _Checkpoint_manager;

    public UnityEvent Grid_change;

    private void Start()
    {
        _Checkpoint_manager = GameObject.FindGameObjectWithTag("CM").GetComponent<Checkpoint_manager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Grid_change.Invoke();
            _Checkpoint_manager.LastCheckpointPos = new Vector2(transform.position.x, transform.position.y);
        }
    }
}
