using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class interactiveObject : MonoBehaviour, IInteractive
{
    public UnityEvent objectEvent;
    public GameObject textInstruction;
    protected Player _player;

    public virtual void PlayEvent(Player player)
    {
        _player = player;
        objectEvent.Invoke();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            textInstruction.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            textInstruction.SetActive(false);
        }
    }
}
