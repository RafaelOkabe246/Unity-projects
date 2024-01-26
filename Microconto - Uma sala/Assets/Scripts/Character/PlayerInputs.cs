using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    [SerializeField] internal Character _character;
    private void Awake()
    {
        _character = GetComponent<Character>();
    }

    private void Update()
    {
        _character.direction.x = Input.GetAxisRaw("Horizontal");

    }
}
