using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class drawn : MonoBehaviour
{
    public List<Vector3> pontos = new List<Vector3>();
    public Action<IEnumerable<Vector3>> OnNewPathCreated = delegate { };
    LineRenderer linha;
    GameObject linhaObj;
    private Mesh mesh = new Mesh();
    MeshCollider meshCollider;
    public Material mate;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            pontos.Clear();
            linhaObj = null;
        }
        if (Input.GetButton("Fire1"))
        {

            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (raycastHit.collider.tag == "Tinta")
                {
                    Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    if (DistanceToLastPoint(mouseWorldPosition) > 1f)
                    {
                        Destroy(linhaObj);
                        mouseWorldPosition.z = 0;
                        pontos.Add(mouseWorldPosition);
                        linhaObj = new GameObject("Linha");
                        linha = linhaObj.AddComponent<LineRenderer>();
                        linha.material = mate;
                        linhaObj.layer = 8;
                        linhaObj.tag = "Tinta";
                        linhaObj.layer = 8;
                        linha.positionCount = pontos.Count;
                        linha.SetPositions(pontos.ToArray());
                        linha.startWidth = 0.60f;
                        meshCollider = linhaObj.AddComponent<MeshCollider>();
                        MeshRenderer meshRen = linhaObj.AddComponent<MeshRenderer>();
                        mesh = linhaObj.AddComponent<MeshFilter>().mesh;
                        linha.BakeMesh(mesh, Camera.main);
                        meshCollider.sharedMesh = mesh;
                    }
                }
                else
                {
                    pontos.Clear();
                    linhaObj = null;
                }
            }
        }
        float DistanceToLastPoint(Vector2 ponto)
        {
            if (!pontos.Any())
            {
                return Mathf.Infinity;
            }
            return Vector2.Distance(pontos.Last(), ponto);
        }
    }
}