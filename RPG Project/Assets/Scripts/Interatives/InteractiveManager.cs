using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractiveManager : MonoBehaviour
{
    public UnityEvent interactAction;
    public KeyCode InteractKey;
    public bool Player_is_range;



    void Update()
    {
        if (Player_is_range)
        {
            if (Input.GetKeyDown(InteractKey))
            {
                interactAction.Invoke();
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
