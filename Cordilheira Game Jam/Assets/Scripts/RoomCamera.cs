using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCamera : MonoBehaviour
{
    private BoxCollider2D manageBox; //Management's BoxCollider
    private Transform player; //Player position
    public GameObject room; //The real camera room which will be activated and deativated

    void Start()
    {
        manageBox = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Transform>();
    }

    void Update()
    {
        ManagerLimits();

    }

    void ManagerLimits()
    {
        if (manageBox.bounds.min.x < player.transform.position.x && player.position.x < manageBox.bounds.max.x &&
           manageBox.bounds.min.y < player.transform.position.y && player.position.y < manageBox.bounds.max.y)
        {
            room.SetActive(true);
        }
        else
        {
            room.SetActive(false);
        }
    }
}
