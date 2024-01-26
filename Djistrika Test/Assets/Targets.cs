using TMPro;
using UnityEngine;

public class Targets : MonoBehaviour
{
    public int verticeValor;    
    public Targets[] verticeDestino;
    public float[] distanciaPeso;
    public TextMeshPro tmp;

    private void Awake()
    {        
        distanciaPeso = new float[verticeDestino.Length];
        for(int i = 0, v = verticeDestino.Length; i < v; i++)
        {
            distanciaPeso[i] = Vector3.Distance(gameObject.transform.position, verticeDestino[i].gameObject.transform.position);
        }
    }

    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    } 
}
