using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {
    public GameObject obstaculo;
    [SerializeField] private float lIferior, lSuperior;
    [Header("Intervalo de spawn")]
    [SerializeField] private float spawnInterval = 2f;
    public int currrentSpawns;
    public int maxSpawns;
    public MinijogoController minijogoController;

    private void Start()
    {
        minijogoController = FindObjectOfType<MinijogoController>();

        InvokeRepeating(nameof(SpawnObject), 6f, spawnInterval);
    }

    private void SpawnObject() {
        float altura = Random.Range(lIferior, lSuperior);

        if(currrentSpawns < maxSpawns)
        {
            Instantiate(obstaculo, new Vector3(12f, altura, 0f), Quaternion.identity);
            currrentSpawns++;
        }
        else
        {
            minijogoController.MinijogoEnded(true);
        }
    }

}
