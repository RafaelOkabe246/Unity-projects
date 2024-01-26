using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsSpawner : MonoBehaviour
{
    public float timeToSpawn = 1f;
    private float timeCount;

    public float minPositionY;
    public float maxPositionY;

    void Update()
    {
        timeCount += Time.deltaTime;
        if (timeCount >= timeToSpawn)
        {
            timeCount = 0;
            float newPositionY = Random.Range(minPositionY, maxPositionY);
            ObjectPooler.Instance.SpawnFromPool("Cloud", new Vector3(transform.position.x, newPositionY, 0f), Quaternion.identity);
        }
    }
}
