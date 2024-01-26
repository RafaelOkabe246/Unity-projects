using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldSpaceCanvas : MonoBehaviour
{
    private Canvas worldSpcCanvas;

    private void Start()
    {
        worldSpcCanvas = GetComponent<Canvas>();
        worldSpcCanvas.worldCamera = Camera.main;
    }
}
