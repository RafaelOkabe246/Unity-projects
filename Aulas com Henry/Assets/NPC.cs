using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 1. Terminar de fazer os métodos de checar posição, apontar e movimentar
 * 2. Mudar o target de acordo com a distância do ponto
 * 3. Aplica-los no update
 * 4. Criar um cubo ou piramide apontando para a frente do NPC e o jogador 
 * 5. Criar novas cores para o jogador e o NPC
*/


/* Se der tempo, exercício de calculadora
 * 
 * 
*/
public class NPC : MonoBehaviour
{
    public Transform pontoA;
    public Transform pontoB;
    //public Transform target;
    public Transform target;

    public float velocidade;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = pontoA.position;
    }

    // Update is called once per frame
    void Update()
    {
       
        apontar();
        movimentar();
        checarPosicao();
    }

    void apontar()
    {
        transform.LookAt(target);
    }

    void movimentar()
    {
        transform.Translate(Vector3.forward * velocidade * Time.deltaTime);
    }

    void checarPosicao()
    {
        if(Vector3.Distance(transform.position, pontoA.position) < 0.1f)
        {
            //Apontar para o ponto b
            target = pontoB;
        }
        else if(Vector3.Distance(transform.position, pontoB.position) < 0.1f)
        {
            //Apontar para o ponto a
            target = pontoA;
        }
    }


}
