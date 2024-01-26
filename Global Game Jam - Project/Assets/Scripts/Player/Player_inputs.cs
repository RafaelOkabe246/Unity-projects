using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_inputs : MonoBehaviour
{
    [SerializeField] internal Player _Player;

    [SerializeField] internal bool isInteracting;
    [SerializeField] internal bool upIsPressed;
    void Update()
    {

        if (_Player.Estado_atual == Modos.Ativo)
        {
            _Player.Horizontal_direction = new Vector2(Input.GetAxis("Horizontal"), 0);

            _Player.Vertical_direction =Input.GetAxis("Vertical");

            if (_Player.isJoana == false)
            {
                if (Input.GetKeyDown(KeyCode.Space) && _Player.Movement.isGrounded == true)
                {
                    //===============
                    //Pular
                    //===============
                    _Player.Movement.JumpForce();
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    upIsPressed = true;
                }
                else if (Input.GetKeyUp(KeyCode.W))
                {
                    upIsPressed = false;
                }
                
            }


            //=================================
            //Interagindo
            //=================================
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Interact
                isInteracting = true;
            }
            else if (Input.GetKeyUp(KeyCode.E))
            {
                isInteracting = false;
            }


            //======================
            // Mudando de personagem
            //======================
            if (Input.GetKeyDown(KeyCode.L))
            {
                //Louis
                _Player.Controller.Personagem_sendo_controlado = Personagem.Louis;
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                //Clara
                _Player.Controller.Personagem_sendo_controlado = Personagem.Clarinha;
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                //Joana
                _Player.Controller.Personagem_sendo_controlado = Personagem.Joana;
            }


        }
    }



}
