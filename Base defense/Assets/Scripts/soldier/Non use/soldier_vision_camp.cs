using Bolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class soldier_vision_camp : MonoBehaviour
{
    [SerializeField] internal soldier soldier_script;
    [SerializeField] internal soldier_enemy_detection enemy_detection;

    private Mesh mesh;

    private void Start()
    {
        //====================
        //Mesh generator
        //====================
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }


    private void Update()
    {
        
        float field_of_view = 90f;
        int rayCount = 50;

        // Rays são os pontos do campo de visão, há três pontos, mas a Unity conta também o ponto zero

        float angle = 45f;
        float angleIncrease = field_of_view / rayCount;
        float viewDistance = 10f;
        Vector3 Origin = Vector3.zero;
        
        angleIncrease = field_of_view / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = Origin;

        int vertexIndex = 1;
        int triangleIndex = 0;

        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex = Origin + UtilsClass.GetVectorFromAngle(angle) * viewDistance;
  
            
            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;
                triangleIndex += 3;
            }
            vertexIndex++;
            angle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;


        
    }

     

    public void Raycast(Vector3 vertex, float angle, float viewDistance)
    {

        RaycastHit2D raycasthit2D = Physics2D.Raycast(Vector3.zero , UtilsClass.GetVectorFromAngle(angle), viewDistance);
        if (raycasthit2D.collider == null)
        {
            vertex = Vector3.zero + UtilsClass.GetVectorFromAngle(angle) * viewDistance;
        }
        else
        {
            vertex = raycasthit2D.point;
        }
    }
  

    //Função para converter o float para um Vector3
    public static Vector3 _GetVectorFromAngle_do_codey_monkey(float angle)
    {
        //angle = 0 -> 360
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));

    }
}
