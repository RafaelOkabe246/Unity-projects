using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_text : MonoBehaviour
{

    public GameObject Texto;
    public GameObject Self;

    private void Start()
    {
        Texto.SetActive(false);
    }

    public void Lights_on()
    {
        Texto.SetActive(true);
        Destroy(Texto.gameObject, 10f);
    }
}
