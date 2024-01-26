using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Faca : MonoBehaviour
{
    public GameObject Botao;
    public UnityEvent Acabou;

    private void Awake()
    {
        Botao.SetActive(false);
    }

    public void Show()
    {
        Botao.SetActive(true);
    }


    public void Fim_de_jogo()
    {
        StartCoroutine(Terminar(3f));
    }

    IEnumerator Terminar(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Application.Quit();
    }
}
