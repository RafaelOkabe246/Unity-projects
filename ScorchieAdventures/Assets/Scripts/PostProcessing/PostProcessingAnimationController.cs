using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessingAnimationController : MonoBehaviour
{
    private Animator anim;

    public static PostProcessingAnimationController instance;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }

    public void CallDaytimeTransition() 
    {
        
    }

    public void CallDashImpactFeedback() 
    {
        anim.SetTrigger("DashImpact");
    }
}
