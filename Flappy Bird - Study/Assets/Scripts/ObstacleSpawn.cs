using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawn : MonoBehaviour
{
    public float maxTimer = 1.5f;
    private float currentTimer;

    public GameObject pipePrefab;
    public float _height;

    // Update is called once per frame
    void Update()
    {
        if(currentTimer > maxTimer && GameManager.instance.currentState == GameState.Gameplay)
        {
            SpawnObstacle();
            currentTimer = 0;
        }
        currentTimer += Time.deltaTime;
    }

    void SpawnObstacle()
    {
        Vector3 positionSpawn = transform.position + new Vector3(0, Random.Range(-_height, _height));
        GameObject pipe = Instantiate(pipePrefab ,positionSpawn , Quaternion.identity);

        Destroy(pipe, 10f);
    }
}
