using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public ActivatableUI pauseUI;
    private GameObject player;
    public static PlayerSpawner instance;
    public StageBlocksHandler stageBlocksHandler;

    void Start()
    {
        instance = this;
        Vector3 positionToSpawn = StageBlocksHandler.savedCurrentBlock.startPoint.position;
        SpawnPlayerAtPosition(positionToSpawn);
    }

    public void SpawnPlayerAtPosition(Vector3 positionToSpawn) 
    {
        if (player != null)
            Destroy(player);

        player = Instantiate(playerPrefab, positionToSpawn, Quaternion.identity);
        player.GetComponent<PlayerController>().pauseUI = pauseUI;
        stageBlocksHandler.playerObj = player;
    }
}
