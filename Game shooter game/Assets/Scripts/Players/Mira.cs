using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mira : MonoBehaviour
{
    public Camera CameraMain;
    internal Vector3 Target;
    public LayerMask Enemy;

    

    void Update()
    {
        Target = CameraMain.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
        transform.position = new Vector2(Target.x, Target.y);
    }

    /*
    public Transform aPt;
    public Transform bPt;

    private void OnDrawGizmos()
    {
        Vector2 a = aPt.position;
        Vector2 b = bPt.position;

        Gizmos.DrawLine(a,b);

        float abDist = Vector2.Distance(a, b);

        Debug.Log("A distância entre a e b é " + abDist);
    }
    */
}
