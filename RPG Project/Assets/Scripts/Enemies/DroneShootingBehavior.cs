using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class DroneShootingBehavior : StateMachineBehaviour
{
    public float timer;

    public Girl Player;

    public Boss _Boss;
    public GameObject Shot;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Girl>();
        _Boss = animator.gameObject.GetComponent<Boss>();


        if (_Boss.islookleft == false)
        {
            //rotation = 180

            Instantiate(Shot, _Boss.shoot_point.position, Shot.transform.rotation);
        }
        else if (_Boss.islookleft == true)
        {
            //rotation = 0

            Instantiate(Shot, _Boss.shoot_point.position, Shot.transform.rotation);
        }

        timer = animator.GetCurrentAnimatorStateInfo(1).length;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
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

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {


    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
