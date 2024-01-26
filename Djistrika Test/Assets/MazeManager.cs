using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    [SerializeField] GameObject[] m_gameObject;
    [SerializeField] Targets[] targets;
    [SerializeField] int origem;
    [SerializeField] int destino;
    Dijkstra dijkstra = new Dijkstra();    
    // Start is called before the first frame update
    void Start()
    {
        // localiza todos os objetos com targets na cena
        m_gameObject = GameObject.FindGameObjectsWithTag("target");

        // inicializa a variável target
        targets = new Targets[m_gameObject.Length];        
        // associa todos os targets à variável de target
        for (int i = 0; i < m_gameObject.Length; i++)
        {
            targets[i] = m_gameObject[i].GetComponent<Targets>();            
        }

                

        // inicializa a quantidade de vértices do dijkstra
        dijkstra.InicializaTamanho(m_gameObject.Length);

        // varre todos os vértices mostrando a origem, destino e distância
        for(int i = 0; i < targets.Length; i++)
        {            
            for(int j = 0; j < targets[i].verticeDestino.Length; j++)
            {
                //Debug.Log("Origem:" + targets[i].verticeValor);
                //Debug.Log("Destino:" + targets[i].verticeDestino[j].verticeValor);
                //Debug.Log("Distância:" + targets[i].distanciaPeso[j]);
                dijkstra.SetaOrigemDestinoPeso(
                    targets[i].verticeValor,
                    targets[i].verticeDestino[j].verticeValor,
                    targets[i].distanciaPeso[j]);
            }                        
        }
        dijkstra.CalcularCaminhoMinimo(origem);
        //dijkstra.MostrarDistancias();
        //dijkstra.MostrarListaAdjacencias();
        
        //Mostrar caminho recebe a origem e o destino.
        
        int[] cam = dijkstra.MostrarCaminho(origem, destino);
        
        /*
        for(int i = 0; i < cam.Length; i++)
        {
            Debug.Log(cam[i]);            
        }
        */
        for(int i = 0; i < targets.Length; i++)
        {
            for(int j = 0; j < cam.Length; j++)
            {
                if (targets[i].verticeValor == cam[j])
                {
                    targets[i].tmp.color = Color.red;
                }
            }
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
