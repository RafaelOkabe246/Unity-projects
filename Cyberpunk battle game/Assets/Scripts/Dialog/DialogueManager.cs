using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


//O armazenamento das falas

//Diferenciar o texto normal do de escolha, e quando aparecer um de escolha automaticamente adicioná-lo nos textos dos botões

//Executar o texto no objeto de dialog

namespace DialogueSystem
{
    //O armazenamento das falas de decisão


    //Diferenciar o texto normal do de escolha, e quando aparecer um de escolha automaticamente adicioná-lo nos textos dos botões


    //Executar o texto no objeto de dialog
    public class DialogueManager : MonoBehaviour
    {

        protected IEnumerator WriteText(string input, Text caixa_de_texto, float delay)
        {
            foreach (char letter in input.ToCharArray())
            {
                caixa_de_texto.text += letter;
                yield return new WaitForSeconds(delay);
            }
        }

    }
}


