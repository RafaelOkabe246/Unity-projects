using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColetorDeLixo : MonoBehaviour
{
    public int lixoColetado;

    public void ColetarLixo()
    {
        lixoColetado += 1;
    }
}
