using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberEcho : MonoBehaviour, IPooledObject
{
    public Animator anim;
    public void OnObjectSpawn()
    {
        anim.Play("Fade", 0, 0f);
    }
}
