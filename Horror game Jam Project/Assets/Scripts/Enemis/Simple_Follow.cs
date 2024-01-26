using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simple_Follow : MonoBehaviour
{
    public bool CanFollow;

    [SerializeField]
    private Vector2 Player;

    [SerializeField]
    private Transform _Player;

    private void Start()
    {
        _Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public void Follow()
    {
        CanFollow = true;
    }

    private void FixedUpdate()
    {
        if (CanFollow == true)
        {
            Movement(Player);
        }
    }

    private void Update()
    {
        Player = new Vector2(_Player.position.x, _Player.position.y);
    }

    void Movement(Vector2 direction)
    {
        transform.position = Vector2.MoveTowards(transform.position, Player, 2.5f * Time.deltaTime);
    }
}
