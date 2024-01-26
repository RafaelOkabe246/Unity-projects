using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] internal Character _character;

    private void Awake()
    {
        _character = GetComponent<Character>();
    }

    public void Movement(Vector2 direction)
    {
        _character.rig.MovePosition((Vector2)transform.position + _character.speed * direction * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        Movement(_character.direction);
    }
}
