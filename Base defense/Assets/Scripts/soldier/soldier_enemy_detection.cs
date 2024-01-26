using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;
using System.Linq;

public class soldier_enemy_detection : MonoBehaviour
{
    [SerializeField] internal soldier _soldier;
    [SerializeField] internal soldier_field_of_view Field_Of_View;
    [SerializeField] Mesh mesh;
    RaycastHit2D hit;
    [SerializeField] float meshRes = 2;

     public Vector3[] vertices;
     public int[] triangles;
     public int stepCount;

    void Start()
    {
        _soldier = GetComponentInParent<soldier>();
        mesh = GetComponent<MeshFilter>().mesh;
    }

    private void LateUpdate()
    {
        MakeMesh();
    }

    void MakeMesh()
    {
        stepCount = Mathf.RoundToInt(Field_Of_View.viewAngle * meshRes);
        float stepAngle = Field_Of_View.viewAngle / stepCount;

        List<Vector3> viewVertex = new List<Vector3>();

        hit = new RaycastHit2D();

        for (int i = 0; i <= stepCount; i++)
        {
            float angle = Field_Of_View.transform.eulerAngles.y - Field_Of_View.viewAngle / 2 + stepAngle * i;
            Vector3 dir = Field_Of_View.DirFromAngle(angle, false);

            //raycasts
            hit = Physics2D.Raycast(Field_Of_View.transform.position,  dir, Field_Of_View.viewRadius, Field_Of_View.obstacleMask);

            if (hit.collider == null)
            {
                viewVertex.Add(transform.position + dir.normalized * Field_Of_View.viewRadius);
            }
            else
            {
                viewVertex.Add(transform.position + dir.normalized * hit.distance);
            }
        }

            int vertexCount = viewVertex.Count + 1;

            vertices = new Vector3[vertexCount];
            triangles = new int[(vertexCount - 2) * 3];

            vertices[0] = Vector3.zero;
        
        
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewVertex[i]);

              if (i < vertexCount - 2)
              {
                triangles[i * 3 + 2] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3] = i + 2;
            }

        }

        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
