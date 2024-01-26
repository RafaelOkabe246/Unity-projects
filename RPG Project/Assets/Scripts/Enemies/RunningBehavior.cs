using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningBehavior : StateMachineBehaviour
{
    public float timer;

    private Girl Player_script;
    public bool islookleft;
    public float speed = 2.5f;

    public Transform player;
    Rigidbody2D Rig;

    public bool CanRun;



    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player_script = GameObject.FindGameObjectWithTag("Player").GetComponent<Girl>();
        player = Player_script.gameObject.GetComponent<Transform>();
        Rig = animator.GetComponent<Rigidbody2D>();
        timer = animator.GetCurrentAnimatorStateInfo(1).length;

        CanRun = true;
    }

     //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            animator.SetTrigger("Idle");
        }
        else
        {
            timer -= Time.deltaTime;
        }

        islookleft = Player_script.islookleft;



        Vector2 Player_position = new Vector2(player.position.x,player.position.y);
        if (CanRun == true)
        {
            Rig.velocity = Player_position * speed; 

        }
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
