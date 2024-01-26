using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorExit : MonoBehaviour
{
    [SerializeField] Collider2D _ownCollider2D;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EndGame();
        }
    }

    void EndGame()
    {
        SceneController.instance.Win();
    }

    public void OpenDoor()
    {
        _ownCollider2D.isTrigger = true;
    }
}
