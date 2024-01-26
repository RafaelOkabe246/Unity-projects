using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fantasma_Trigger : MonoBehaviour
{
    private BoxCollider2D Self;
    public bool CanAttack;

    private void Start()
    {
        Self = this.GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Self.gameObject.SetActive(false);
            CanAttack = true;
        }
    }
}
