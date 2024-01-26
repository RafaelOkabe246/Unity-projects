using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsSpawner : MonoBehaviour
{
    IEnumerator Start()
    {
        Invoke(nameof(SpawnObjects), 1f);
        yield return new WaitForSeconds(1f);
    }

    private void SpawnObjects()
    {

    }

    void ObjectSpawn()
    {

    }
}
