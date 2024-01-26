﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBossBehaviour : StateMachineBehaviour
{
    private Rigidbody2D Boss_Rig;
    private int seletor;
    public float timer;
    public float minTime = 1f;
    public float maxTime = 2f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = Random.Range(minTime, maxTime);
        Boss_Rig = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(timer <= 0)
        {
            seletor = Random.Range(1,1);

            if(seletor == 1)
            {
                animator.SetTrigger("Jumping");
            }
            else if(seletor == 2)
            {
                animator.SetTrigger("Dashing");
            }
            else if(seletor == 3)
            {
                animator.SetTrigger("Attacking");
            }

        }
        else
        {
            timer -= Time.deltaTime;
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
