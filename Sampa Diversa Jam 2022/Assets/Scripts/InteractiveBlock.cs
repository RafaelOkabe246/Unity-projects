using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveBlock : MonoBehaviour
{
    private Animator animator;

    public void Activating()
    {
        animator.SetBool("isIdle",true);
    }
}
