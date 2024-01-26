using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatilhoDeLixo : MonoBehaviour
{
    public void AdicionarLixo()
    {
        ColetorDeLixo c = FindObjectOfType<ColetorDeLixo>();

        c.ColetarLixo();

        Destroy(gameObject);
    }
}
