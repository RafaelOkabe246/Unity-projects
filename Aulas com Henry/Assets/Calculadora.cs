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
        Debug.Log("A raiz quadrada do n�mero2 � : " + Mathf.Sqrt(numero2));
    }


    void Potenciacao()
    {
        //O primeiro n�mero que vc escreve � a base e o segundo n�mero que voc� escreve � o expoente
        Debug.Log("A pot�ncia do " + numero2 + " sob o expoente de " + numero1 + " �: " + Mathf.Pow(numero1, numero2));

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
