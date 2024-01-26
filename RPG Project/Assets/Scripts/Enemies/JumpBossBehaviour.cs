using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBossBehaviour : StateMachineBehaviour
{
    public float timer;
    Rigidbody2D Rig;

    [SerializeField]
    private Transform PlayerPos;


    public Girl Player;

    public Boss _Boss;

    public float speed;
    public float jumpforce;


    [Header("Jumping")]
    [SerializeField] float JumpHeight;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private bool isGrounded;

    public bool Pare;

    [SerializeField]Vector2 Target;
    [SerializeField] Vector2 VecBoss;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _Boss = animator.gameObject.GetComponent<Boss>();
        groundCheck = _Boss.groundCheck;

        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Girl>();
        PlayerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Rig = animator.GetComponent<Rigidbody2D>();
        //groundCheck = GameObject.FindGameObjectWithTag("GroundCheck").GetComponent<Transform>();

        timer = animator.GetCurrentAnimatorStateInfo(1).length;

        JumpAttack();

        Pare = false;

        //Target = new Vector2(PlayerPos.position.x, animator.transform.position.y);
        //animator.transform.position = Vector2.MoveTowards(animator.transform.position, Target, speed * Time.deltaTime);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        VecBoss = new Vector2(animator.transform.position.x, animator.transform.position.y);
        isGrounded = _Boss.isGrounded;

        if (timer <= 0)
        {
            Rig.velocity += Vector2.zero;
            animator.SetTrigger("Idle");
        }
        else
        {
            timer -= Time.deltaTime;
        }


        //Jumping


        //Vector2 target = new Vector2(PlayerPos.position.x, animator.transform.position.y);
        //animator.transform.position = Vector2.MoveTowards(animator.transform.position, target, speed * Time.deltaTime);
    }

    void JumpAttack()
    {
        float PlayerToBoss = PlayerPos.position.x - _Boss.gameObject.transform.position.x;

        if (isGrounded == true)
        {
            speed -= Time.deltaTime;
            Rig.velocity += Vector2.up * JumpHeight;
            //Rig.velocity += new Vector2(,0);
            //_Boss.gameObject.transform.position = Vector2.MoveTowards(_Boss.gameObject.transform.position, Target, speed * Time.deltaTime);
        }
        else
        {
            speed -= Time.deltaTime;
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
