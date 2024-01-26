using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculadora : MonoBehaviour
{
    public float numero1;
    public float numero2;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
            RaizQuadrada();
        if (Input.GetKeyDown(KeyCode.S))
            Potenciacao();
    }

    void RaizQuadrada()
    {
        Debug.Log("A raiz quadrada do número2 é : " + Mathf.Sqrt(numero2));
    }


    void Potenciacao()
    {
        //O primeiro número que vc escreve é a base e o segundo número que você escreve é o expoente
        Debug.Log("A potência do " + numero2 + " sob o expoente de " + numero1 + " é: " + Mathf.Pow(numero1, numero2));

    }


    double resultado = 1;/*
    int OutroMetodoPotenciacao(double numeroBase, int numeroExpoente)
    {

    }*/

    public void Adicao()
    {
        Debug.Log(numero1 + numero2);
    }
}
