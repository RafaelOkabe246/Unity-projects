using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desativer : MonoBehaviour {
    [SerializeField] private string tagColisao;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(tagColisao))
        {
            //Algo acontece, por enquanto vamo destruir as bolinha
            Destroy(gameObject);
        }
    }
}
