using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//============================
//  Use esse código para mudar o modo do personagem
//============================
public class soldier_mode : MonoBehaviour
{
    [SerializeField] internal soldier soldier_script;
    //Começa numa posição reserva que esteja "null".
    //
    //Como:
    //Pegue todos os objetos de posição para o jogador que sejam "reserva", coloque a quantidade de posições no "int posições"
    //
    //Ele vai procurar por objetos com a tag "position", ver qual qual tem a bool "posição" e está "=! null"
    //Se encontrar um null, vá até a posição.
    //Se não encontrar um null, vá para um "null reserva"

    //Três modos: interagindo, movendo e atirando.
    //
    //1) Atirando:
    // Terá uma área x em que se aparecer um, ou mais, inimigos, o soldado vai atirar nos três mais próximos da base
    //2) Interagindo:
    // O soldado ativará a animação interagindo e o script do elemento.
    //3) Movendo:
    // O soldado irá até a posição do elemento, ou posição, "elemento.soldadoTransform". Se é elemento, faça interagindo; se for
    // posição, pare e inicie atirando 

    public enum Modo
    {
        Atirando,
        Movendo,
        Selecionado,
        Interagindo,
    };

    public Modo Modo_atual;



}
