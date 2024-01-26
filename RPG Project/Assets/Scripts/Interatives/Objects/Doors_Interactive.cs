using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Doors_Interactive : MonoBehaviour
{
    public UnityEvent interactAction;
    public KeyCode InteractKey;
    public bool Player_is_range;

    private PlayerManager _PlayerManager;

    void Start()
    {
        _PlayerManager = FindObjectOfType(typeof(PlayerManager)) as PlayerManager;
    }


    void Update()
    {
        if (Player_is_range)
        {
            if (Input.GetKeyDown(InteractKey))
            {
                if(_PlayerManager.TasksComplete == true)
                {
                    interactAction.Invoke();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player_is_range = true;
            Debug.Log("Player interagindo");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player_is_range = false;
            Debug.Log("Player não está interagindo");
        }
    }
}
