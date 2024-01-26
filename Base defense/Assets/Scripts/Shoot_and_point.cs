using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot_and_point : MonoBehaviour
{
    public GameObject Selected_soldier;

    public GameObject Player_Character;
    public GameObject Mira;
    private Vector3 Target;


    public LayerMask Soldier;

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        Target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
        Mira.transform.position = new Vector2(Target.x, Target.y);

        Vector3 distance_different = Target - Player_Character.transform.position;
        float rotationZ = Mathf.Atan2(distance_different.y, distance_different.x) * Mathf.Rad2Deg;
        Player_Character.transform.rotation = Quaternion.Euler(0f,0f,rotationZ);
    }



}
