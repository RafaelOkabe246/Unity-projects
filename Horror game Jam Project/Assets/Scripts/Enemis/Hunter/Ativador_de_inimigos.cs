using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ativador_de_inimigos : MonoBehaviour
{
    public UnityEvent Active_enemy;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           // Active_enemy.Invoke();
        }
        
    }
}
