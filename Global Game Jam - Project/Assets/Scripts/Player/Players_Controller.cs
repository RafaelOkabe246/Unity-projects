using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Personagem
{
    Clarinha,
    Louis,
    Joana,
    Juntos,
};

public class Players_Controller : MonoBehaviour
{
    [SerializeField] internal Player Clara;
    [SerializeField] internal Player Louis;
    [SerializeField] internal Player Joana;

    public Personagem Personagem_sendo_controlado;

    [SerializeField] internal GameObject Personagem_ativo;

    [Header("Camera")]
    [SerializeField] internal CinemachineVirtualCamera Cam_v;

    [Header("Cloud talk objcts")]
    [SerializeField] internal GameObject P_selecionado;
    [SerializeField] internal GameObject Exclamacao;
    [SerializeField] internal GameObject Interrogacao;
    [SerializeField] internal GameObject N_passar;


    private void Update()
    {

        if (Personagem_sendo_controlado == Personagem.Clarinha)
        {
            Clara.Estado_atual = Modos.Ativo;
            Joana.Estado_atual = Modos.Passivo;
            Louis.Estado_atual = Modos.Passivo;

            Cam_v.Follow = Clara.groundCheckPosition;

            P_selecionado.transform.position = Clara.CloudTalk.transform.position;

            Personagem_ativo = Clara.gameObject;
        }
        if (Personagem_sendo_controlado == Personagem.Louis)
        {
            Louis.Estado_atual = Modos.Ativo;
            Joana.Estado_atual = Modos.Passivo;
            Clara.Estado_atual = Modos.Passivo;

            Cam_v.Follow = Louis.groundCheckPosition;

            P_selecionado.transform.position = Louis.CloudTalk.transform.position;

            Personagem_ativo = Louis.gameObject;
        }
        if (Personagem_sendo_controlado == Personagem.Joana)
        {
            Joana.Estado_atual = Modos.Ativo;
            Clara.Estado_atual = Modos.Passivo;
            Louis.Estado_atual = Modos.Passivo;

            Cam_v.Follow = Joana.groundCheckPosition;

            P_selecionado.transform.position = Joana.CloudTalk.transform.position;

            Personagem_ativo = Joana.gameObject;
        }

    }


}
