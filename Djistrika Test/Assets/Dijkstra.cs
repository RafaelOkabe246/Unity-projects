using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Dijkstra
{
    // classes
    public class Vertice
    {
        public int num;
        public float peso;
        public Vertice prox;        
    }

    public class ListaAdj
    {
        public Vertice listaV;
    }

    // variáveis
    public int tam = 0,   // número de vértices do grafo
    org = 0,   // vértice de origem da aresta
    dest = 0,  // vértice de destino da aresta            
    op = 0,    // opção do menu
    num = 0,   // vértice de origem do caminho mínimo
    flag = 0;  // variável para validação do menu
    public float peso = 0f;  // peso da aresta do grafo
    Vertice novo; 
   
    public ListaPrioridades lista; // lista de prioridades    
    public int[] marcado;          // vetor para marcar visitacao dos vertices
    public int[] pai;              // antecessor dos vértices no caminho mínimo
    public float[] dist;             // distância mínima em relação à origem
    public ListaAdj[] adj;         // lista de adjacências dos vértices

    public void InicializaTamanho(int tm)
    {
        tam = tm;               
        adj = new ListaAdj[tam + 1];
        marcado = new int[tam + 1];
        pai = new int[tam + 1];
        dist = new float[tam + 1];

        for (int i = 1; i <= tam; i++)
        {
            adj[i] = new ListaAdj();
            marcado[i] = 0;
        }
    }

    public void SetaOrigemDestinoPeso(int origem, int destino, float distancia)
    {
        org = origem;
        dest = destino;
        peso = distancia;

        novo = new Vertice();
        novo.num = dest;
        novo.peso = peso;
        // inserindo vértice adjacente a vértice
        // origem na lista de adjacências
        novo.prox = adj[org].listaV;
        adj[org].listaV = novo;
    }

    public void CalcularCaminhoMinimo(int origem)
    {
        num = origem;
        for (int i = 1; i <= tam; i++)
        {
            marcado[i] = 0;
            dist[i] = 0;
        }
        ChamaDijkstra(adj, tam, num);
        flag = 1;
    }

    public void MostrarListaAdjacencias()
    {
        MostrarAdj(adj, tam);
    }

    public void MostrarDistancias()
    {
        if (flag == 0)
        {
            Debug.Log("É necessário realizar a busca primeiro!");
        }
        else
        {
            MostrarDist(tam, num);
        }
    }

    public void ChamaDijkstra(ListaAdj[] adj, int tam, int v)
    {
        int w = -1;
        int[] c = new int[tam];
        int tamC = 0;
        lista = new ListaPrioridades(tam);

        dist[v] = 0;
        lista.Inserir(v, dist);
        // cria a lista com todos as distancias infinitas
        for (int i = 1; i <= tam; i++)
        {
            if (i != v)
            {
                // max value se refere ao infinito
                dist[i] = int.MaxValue;
                pai[i] = 0;
                lista.Inserir(i, dist);                
            }
        }

        while (lista.tam != 0)
        {            
            w = lista.Remover(dist);            
            c[tamC] = w;
            tamC++;

            Vertice x = adj[w].listaV;
            
            
            
            
            
            while (x != null)
            {
                // relax (w, x, peso_wx)
                Relax(w, x.num, x.peso);
                // próximo vizinho de w                
                x = x.prox;
            }           
            lista.ConstroiHeap(dist);
        }
    }

    public void Relax(int u, int v, float peso)
    {        
        if (dist[v] > dist[u] + peso)
        {
            dist[v] = dist[u] + peso;
            pai[v] = u;
        }
    }

    public void MostrarAdj(ListaAdj[] adj, int tam)
    {
        Vertice v;
        for (int i = 1; i <= tam; i++)
        {
            v = adj[i].listaV;
            Debug.Log("Entrada " + i + " ");
            while (v != null)
            {
                Debug.Log("(" + i + "," + v.num + ")" + "");
                v = v.prox;
            }
        }
    }

    public void MostrarDist(int tam, int or)
    {
        Debug.Log("Distância da origem " + or + " para os demais vértices:\n");
        for (int i = 1; i <= tam; i++)
        {
            Debug.Log("" + i + "-" + dist[i]);
        }        
    }

    // compara qual o vértice mais próximo da origem, partindo do destino
    // e adiciona na pilha.
    public int[] MostrarCaminho(int origem, int destino)
    {
        Stack<int> caminho = new Stack<int>();        
        int destinoTemp = destino;
        caminho.Push(destinoTemp);
        /*
        for(int i = 1; i <  lista.listaDistancias.Length; i++)
        {
            Debug.Log("Vetor: " + i + " - Distância: " + lista.listaDistancias[i]);
        }*/

        // para cada vértice, a partir do destino, testa-se para saber
        // qual tem a menor distância da origem

        Vertice vert = new Vertice();
        while (destinoTemp != origem)
        {
            vert = adj[destinoTemp].listaV;
            Stack<int> chave = new Stack<int>();
            Stack<float> valor = new Stack<float>();
            chave.Push(destinoTemp);
            valor.Push(lista.listaDistancias[destinoTemp]);
            while (vert != null)
            {
                //Debug.Log("Vertice Numero" + vert.num);
                //Debug.Log("Vertice Peso" + vert.peso);
                //Debug.Log("Vertice Peso" + dist[vert.num]);
                chave.Push(vert.num);
                valor.Push(lista.listaDistancias[vert.num]);
                vert = vert.prox;

                // daqui pega-se o vertice com menor peso
            }
            destinoTemp = RetornaMenorValor(chave.ToArray(), valor.ToArray());
            caminho.Push(destinoTemp);
            //destinoTemp = origem;
            //Debug.Log("Destino Temp " + destinoTemp);
        }
        return caminho.ToArray();
    }

    // retorna o menor valor entre os pesos
    public int RetornaMenorValor(int[] chave, float[] valor)
    {
        float tempValor = valor.Min();
        //Debug.Log(tempValor);
        int retorno = 0;
        for(int i = 0; i < chave.Length; i++)
        {
            if (tempValor == valor[i]) retorno = chave[i];
        }
        return retorno;
    }
}
