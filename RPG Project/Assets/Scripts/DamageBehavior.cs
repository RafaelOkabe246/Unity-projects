using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBehavior : StateMachineBehaviour
{
    public Color HitColor;
    public Color NormalColor;

    [SerializeField]
    private Animator Player;

    private SpriteRenderer _SR;
    Rigidbody2D PlayerRig;
    public float Force;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _SR = animator.GetComponent<SpriteRenderer>();
        PlayerRig = animator.GetComponent<Rigidbody2D>();
        PlayerRig.AddForce(-animator.transform.right * Force);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player = animator;
        Damage();
    }

    IEnumerator Damage()
    {

        _SR.color = HitColor;
        yield return new WaitForSeconds(0.2f);
        _SR.color = NormalColor;
        yield return new WaitForSeconds(0.2f);
        _SR.color = HitColor;
        yield return new WaitForSeconds(0.2f);
        _SR.color = NormalColor;
        yield return new WaitForSeconds(0.2f);

        Player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _SR.color = NormalColor;
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
