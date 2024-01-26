using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemys : MonoBehaviour
{
    public GameObject Normal_enemy;

    public float spawnrate;
    public float nextTimeToSpawn;

    public bool CanSpawn;

    public Transform[] Spawn_points;



    void Start()
    {
        CanSpawn = true;
        spawnrate = 0.5f;
    }

    void Update()
    {

        if (Time.time >= nextTimeToSpawn && CanSpawn == true)
        {
            Hexagon_Spawn();
        }
        
    }

    void Hexagon_Spawn()
    {
        int position = Random.Range(0, Spawn_points.Length);

        Instantiate(Normal_enemy, Spawn_points[position].position, Quaternion.identity);
        nextTimeToSpawn = Time.time + 1f / spawnrate;
    }
}
