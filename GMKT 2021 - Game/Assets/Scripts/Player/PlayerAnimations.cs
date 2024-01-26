using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] internal Player _character;
    [SerializeField] internal Animator _animator;
    private void Awake()
    {
        _character = GetComponent<Player>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(_character.Xspeed != 0)
        {
            _character.isRunning = true;
        }
        else if(_character.Xspeed == 0)
        {
            _character.isRunning = false;
        }

        _animator.SetFloat("Yspeed", _character.Yspeed);
        _animator.SetBool("isGrounded", _character.isGrounded);
        _animator.SetFloat("Xspeed", _character.Xspeed);
        _animator.SetBool("isRunning", _character.isRunning);
    }

}
