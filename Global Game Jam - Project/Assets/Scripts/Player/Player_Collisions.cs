using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Collisions : MonoBehaviour
{
    [SerializeField] internal Player _Player;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 4)
        {

            if (_Player.Controller.Personagem_sendo_controlado == Personagem.Clarinha)
            {
                _Player.Movement.canDie = false;
                _Player.Rig.velocity = new Vector2(_Player.Rig.velocity.x, 0);
                _Player.transform.position = _Player.Respawn.position;
            }
            if (_Player.Controller.Personagem_sendo_controlado == Personagem.Joana)
            {
                _Player.Movement.canDie = false;
                _Player.Rig.velocity = new Vector2(_Player.Rig.velocity.x, 0);
                _Player.transform.position = _Player.Respawn.position;
            }

        }


        if (collision.gameObject.CompareTag("Alavanca"))
        {
            _Player._Alavanca = collision.gameObject.GetComponent<Alavanca>();
            _Player._Alavanca.Event();
        }

        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            _Player.Respawn.position = collision.gameObject.transform.position;
        }
    }
}
