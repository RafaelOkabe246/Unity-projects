using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListaPrioridades
{
    public int[] vet;
    public int tam;    
    public float[] listaDistancias;
    public ListaPrioridades(int n)
    {
        vet = new int[n + 1];        
        listaDistancias = new float[n+1];
        tam = 0;
    }

    public void Inserir(int num, float[] dist)
    {
        int ind = 0;
        if (tam < vet.Length - 1)
        {
            tam++;
            ind = tam;
            while (ind > 1 && dist[vet[Pai(ind)]] > dist[num])
            {
                vet[ind] = vet[Pai(ind)];
                ind = Pai(ind);
            }
            vet[ind] = num;
        }        
    }

    public int Pai(int x)
    {
        return x / 2;
    }

    public void HeapFica(int i, int qtde, float[] dist)
    {
        int f_esq, f_dir, menor, aux;
        menor = i;

        if (2 * i + 1 <= qtde)
        {
            // o nó que está sendo analisado
            // tem filhos para esquerda e direita
            f_esq = 2 * i;
            f_dir = 2 * i + 1;
            if (dist[vet[f_esq]] < dist[vet[f_dir]] && dist[vet[f_esq]] < dist[vet[i]])
            {
                menor = 2 * i;
            }
            else if (dist[vet[f_dir]] < dist[vet[f_esq]] && dist[vet[f_dir]] < dist[vet[i]])
            {
                menor = 2 * i + 1;
            }
        }
        else if (2 * i <= qtde)
        {
            // o nó que está sendo analisado tem
            // filho apenas para a esquerda
            f_esq = 2 * i;
            if (dist[vet[f_esq]] < dist[vet[i]])
            {
                menor = 2 * i;
            }
        }

        if (menor != i)
        {
            aux = vet[i];
            vet[i] = vet[menor];
            vet[menor] = aux;            
            HeapFica(menor, qtde, dist);
        }        
    }

    public void ConstroiHeap(float[] dist)
    {
        for (int i = tam / 2; i >= 1; i--)
        {            
            HeapFica(i, tam, dist);            
        }
    }

    public int Remover(float[] dist)
    {
        if (tam == 0) Debug.Log("Lista vazia!");
        else
        {
            int menor_prior = vet[1];
            //Debug.Log(menor_prior + "Distancia: " + dist[menor_prior]);
            vet[1] = vet[tam];
            tam--;
            HeapFica(1, tam, dist);
            listaDistancias[menor_prior] = dist[menor_prior];
            //Debug.Log("Vet1: " + menor_prior);
            //Debug.Log("Dist " + dist[menor_prior]);
            return menor_prior;
        }
        return 0;
    }

    public void Imprimir()
    {
        for (int i = 1; i <= tam; i++) Debug.Log(" " + vet[i]);
    }
}
