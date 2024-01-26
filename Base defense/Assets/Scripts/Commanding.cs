using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;

public class Commanding : MonoBehaviour
{
    [SerializeField]
    private GameController _GameController;

    [SerializeField] GameObject[] soldiersInScene;

    //Selecionar um soldado espacífico para ficar no modo "comandando"
    // 1)Clique no botão "x", em seguida irá ativar um seletor entre os soldados, esse seletor vai estar no último soldado no momento
    //   que o jogador saíu do modo "comandando".
    // 2)Se o jogador clicar no botão "y", o soldado selecionado vai entrar no modo "selecionado". Há duas ordens que
    //   que podem ser dadas:
    //   1.Mover para a posição que o mouse selecionar, mas se o quadrado, espaço, estiver ocupado o jogador irá sair do modo "comandando",  
    //    caso contrário o soldado irá andar até aquela posição e o jogador vai sair do modo "comandando"
    //   2.Você pode mandar o seu soldado interagir com um elemento no cenário, caso esteja " =! null", saia do modo "comandando", senão   
    //    o soldado irá até o elemento e ativará o script do elemento.


    //Mandando o soldados para a posição
    //1) Clique no "x", assim você entrará no modo "comandando"
    //2) Clique com o botão esquerdo do mouse no soldado
    //3) Ele vai estar selecionado
    //4) Clique na posição livre
    //5) O modo "comandando" vai ser desativado e o soldado vai mudar para "movendo"


    private void Start()
    {
        _GameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            soldier Soldado_selecionado;

            if (Physics.Raycast(ray, out hit, 50f))
            {
                if (hit.transform.gameObject.CompareTag("Soldier"))
                {
                    //Selecione o soldado
                    Soldado_selecionado = hit.transform.gameObject.GetComponent<soldier>();
                    
                }
            }
        }

    }

    
    
}


