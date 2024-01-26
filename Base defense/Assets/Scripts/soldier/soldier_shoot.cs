using MiscUtil.Linq.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//============================
//  Use esse código para o personagem atirar 
//============================
public class soldier_shoot : MonoBehaviour
{
    [SerializeField] internal soldier soldier_script;

    bool canShoot;
    public LayerMask EnemyLayer;

    private void Start()
    {
        soldier_script = GetComponent<soldier>();
    }

    //1)O personagem vai ter uma área no formato de cone em que ele vai enxergar os inimigos
    //1.1)Se não tiver inimigos, ele simplesmente vai ficar parado, irá rodar a animação "idle" até a bool HasEnemy == true
    //1.2)Se ver um inimigo, ou mais, adicione a posição deles para a "lista dos GameObjects 3 inimigos"
    //1.3)1 inimigo ---> Transform inimigo = inimigo
    //1.4)2 ou 3 inimigos ---> Random.Range(1,3) e use o int para selecionar o inimigo da lista e enviar o resultado "Transform inimigo" para o próximo passo

    //2)Ele vai atirar aleatoriamente nos três mais próximos da barricada
    //2.1)Pegue a posição do inmigo
    //2.2)Olhe para a posição do inmigo
    //2.3)Spawne a profab bullet na posição do inimigo
    //2.4)Inicie a animação atirando
    //2.5)Toque o som do tiro

    //3)Ele vai esperar 0.8 segundos para recarregar e então recomeçar a função
    //3.1)Ele vai esperar 0.8 segundos para recarregar
    //3.2)Repetir o passo 1


    public void Shooting(Transform enemy)
    {
        Instantiate(soldier_script.Bullet, enemy.position, Quaternion.identity);
    }
}
