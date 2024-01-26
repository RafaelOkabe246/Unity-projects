using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBallSpawn : MonoBehaviour
{
    public GameObject DeathBall;
    [SerializeField]
    private float SpawnTime = 4f;
    [SerializeField]
    private float Timer;
    public float DeathBallTimer;

    private void Start()
    {
        DeathBall.GetComponent<DeathBallScript>().lifeTime = DeathBallTimer;
    }

    private void Update()
    {
        CreateBall();
    }


    void CreateBall()
    {
        if (Timer >= SpawnTime)
        {
            Instantiate(DeathBall, transform.position, Quaternion.identity);
            Timer = 0;
        }
        else
        {
            Timer += Time.deltaTime;
        }
    }
}
