using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Alavanca : MonoBehaviour
{
    public UnityEvent Evento;

    [SerializeField] internal bool StartEvent;

    [SerializeField] private Animator Anim;

    private void Start()
    {
        Anim = this.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            StartEvent = true;
            
        }
    }

    private void Update()
    {
        if(StartEvent == true)
        {
            Anim.SetTrigger("Acesa");
            Event();
        }
    }

    public void Event()
    {
        Evento.Invoke();
    }
}
