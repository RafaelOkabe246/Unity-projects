using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorZumbis : MonoBehaviour
{
    public GameObject Zumbi;
    public float SpawnZombieRate = 2f;
    public float SpawnZombieTime;
    public LayerMask LayerZumbi;
    public LayerMask LayerCenario;
    private float distanciaDeGeracao = 3;
    private float DistanciaDoJogadorParaGeracao = 20;
    private GameObject jogador;

    // Start is called before the first frame update
    void Start()
    {
        SpawnZombieTime = 0f;
        jogador = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, jogador.transform.position) > DistanciaDoJogadorParaGeracao)
        {
            if (SpawnZombieTime >= SpawnZombieRate)
            {
                StartCoroutine(GerarNovoZumbi());
                SpawnZombieTime = 0f;
            }
            SpawnZombieTime += Time.deltaTime;
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaDeGeracao);
    }

    IEnumerator GerarNovoZumbi()
    {
        Vector3 posicaoDeCriacao = AleatoriarPosicao();
        Collider[] colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, LayerZumbi);
        while(colisores.Length > 0)
        {
            posicaoDeCriacao = AleatoriarPosicao();
            colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, LayerZumbi);
            yield return null;
        }
        Instantiate(Zumbi, posicaoDeCriacao, transform.rotation);
    }

    Vector3 AleatoriarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * 3;
        posicao += transform.position;
        posicao.y = 0;

        return posicao;
    }
}
