using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEffect : MonoBehaviour
{
    public float timeBtwSpawns;
    public float startTimeBtwSpawns;

    ObjectPooler objectPooler;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    void Update()
    {
        if (timeBtwSpawns <= 0)
        {
            objectPooler.SpawnFromPool("BinaryEcho", transform.position, Quaternion.identity);
            timeBtwSpawns = startTimeBtwSpawns;
        }
        else {
            timeBtwSpawns -= Time.deltaTime;
        }
    }
}
