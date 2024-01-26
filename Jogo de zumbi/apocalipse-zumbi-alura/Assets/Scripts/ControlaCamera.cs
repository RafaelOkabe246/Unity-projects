using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaCamera : MonoBehaviour
{
    public GameObject Jogador;
    Vector3 distCompensar;

    // Start is called before the first frame update
    void Start()
    {
        distCompensar = Jogador.transform.position - transform.position;   
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Jogador.transform.position - distCompensar;
    }
}
