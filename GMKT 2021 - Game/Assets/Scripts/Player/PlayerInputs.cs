using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    [SerializeField] internal Player _character;
    private void Awake()
    {
        _character = GetComponent<Player>();
    }

    private void Update()
    {
        if (Time.time >= _character.nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _character.Attack();
                _character.nextAttackTime = Time.time + 1f / _character.AttackRate;
            }
        }
    }
}
