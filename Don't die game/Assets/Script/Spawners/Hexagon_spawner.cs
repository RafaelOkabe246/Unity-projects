using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon_spawner : MonoBehaviour
{
    private GameController _GameController;


    public GameObject Hexagon;

    public float spawnrate;
    private float nextTimeToSpawn = 1f;

    public bool CanSpawn;

    void Start()
    {
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
        CanSpawn = true;
        spawnrate = 0.5f;
}

    void Update()
    {
        if(Time.time >= nextTimeToSpawn && CanSpawn == true)
        {
            Hexagon_Spawn();
        }
        else if(_GameController.currentTime >= 50)
        {
            spawnrate = 0.7f;
        }
      
        if(_GameController.currentTime >= 90)
        {
            CanSpawn = false;
        }
    }

    void Hexagon_Spawn()
    {
        Instantiate(Hexagon, Vector3.zero, Quaternion.identity);
        nextTimeToSpawn = Time.time + 1f / spawnrate;
    }


}
