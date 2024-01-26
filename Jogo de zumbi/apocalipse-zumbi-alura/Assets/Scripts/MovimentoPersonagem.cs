using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPersonagem : MonoBehaviour
{
    private Rigidbody meuRigidbody;

    private void Awake()
    {
        meuRigidbody = GetComponent<Rigidbody>();
    }
    public void Movimentar(Vector3 direcao, float velociadade)
    {
        meuRigidbody.MovePosition(GetComponent<Rigidbody>().position + (direcao.normalized * velociadade * Time.deltaTime));
    }

    public void Rotacionar(Vector3 direcao)
    {
        Quaternion novaRotacao = Quaternion.LookRotation(direcao);
        meuRigidbody.MoveRotation(novaRotacao);
    }
}
