using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactives : MonoBehaviour
{

    private Animator Interactive_animator;

    private bool Is_colliding_to_player;
    public bool Is_Finished;


    public void Tasks_completed()
    {
        Is_Finished = true;
    }

    public void Interaction_completed()
    {
        Is_Finished = true;
        Debug.Log("Funcionou");
    }

    void Start()
    {
        Interactive_animator = GetComponent<Animator>();
    }

    void Update()
    {
        //Animator variables
        Interactive_animator.SetBool("Is_colliding_to_player", Is_colliding_to_player);
        Interactive_animator.SetBool("Is_Finished", Is_Finished);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Is_colliding_to_player = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Is_colliding_to_player = false;
        }

    }


}
