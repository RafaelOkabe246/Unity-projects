using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower_spawner : MonoBehaviour
{
    private GameController _GameController;


    public GameObject Flower;

    public float spawnrate = 1f;
    private float nextTimeToSpawn = 1f;

    public bool CanSpawn;

    void Start()
    {
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
        CanSpawn = true;
    }

    void Update()
    {
        if (Time.time >= nextTimeToSpawn && CanSpawn == true)
        {
            Flower_Spawn();
        }

        if (_GameController.currentTime >= 85)
        {
            CanSpawn = false;
        }
    }

    void Flower_Spawn()
    {
        Instantiate(Flower, Vector3.zero, Quaternion.identity);
        nextTimeToSpawn = Time.time + 1f / spawnrate;
    }


}
