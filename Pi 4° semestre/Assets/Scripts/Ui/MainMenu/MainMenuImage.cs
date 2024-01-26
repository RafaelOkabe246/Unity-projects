using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuImage : MonoBehaviour
{
    public Animator anim;

    public void EnableAnimation(bool value) 
    {
        anim.SetBool("Enabled", value);
    }
}
