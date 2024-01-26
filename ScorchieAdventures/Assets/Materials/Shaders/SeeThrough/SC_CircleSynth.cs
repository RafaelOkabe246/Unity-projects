using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CircleSynth : MonoBehaviour
{
    public static int PosID = Shader.PropertyToID("_Position");
    public static int SizeID = Shader.PropertyToID("_Size");

    public Material wallMaterial;
    private Camera theCamera;
    public LayerMask mask;


    private void Awake()
    {
        theCamera = Camera.main;
    }
    private void Update()
    {
        //Checking if there's a wall in front of the player's view
        Vector2 dir = theCamera.transform.position - transform.position;
        Ray2D ray = new Ray2D(transform.position, dir.normalized);

        if (Physics2D.Raycast(transform.position, dir, 1000000, mask))
        {
            wallMaterial.SetFloat(SizeID, 0.75f);
        }
        else
            wallMaterial.SetFloat(SizeID, 0);

        //Defining the shader position
        var view = theCamera.WorldToViewportPoint(new Vector3(transform.position.x, transform.position.y, 0f));
        wallMaterial.SetVector(PosID, view);
    }
}
