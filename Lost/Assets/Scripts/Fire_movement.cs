using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_movement : MonoBehaviour
{
    public float speed;
    
    private float waittime;
    public float StartWaitTime;

    public Transform[] moveSpots;
    private int randomSpot;


    void Start()
    {
        waittime = StartWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
        {
            if (waittime <= 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length);
                waittime = StartWaitTime;
            }
        }
        else
        {
            waittime -= Time.deltaTime;
        }
    } 

    void Move()
    {

    }

}
