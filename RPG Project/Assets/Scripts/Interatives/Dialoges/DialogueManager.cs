using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{

    public UnityEvent interactAction;
    public KeyCode InteractKey;

    public bool Player_is_range;
    public static bool Is_talking;
    public static bool Start_talking;

    public GameObject self;

    void Start()
    {
        Is_talking = false;
    }


    void Update()
    {
        if (Player_is_range && Is_talking == false)
        {
                if (Input.GetKeyDown(InteractKey))
                {
                    interactAction.Invoke();
                    Is_talking = true;
                }
        }
        if(Is_talking == true)
        {
            Girl.canMove = false;
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
