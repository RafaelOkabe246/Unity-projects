using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_PointClick : MonoBehaviour
{
    public Inventory _Inventory;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // Casts the ray and get the first game object hit
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.name);
            }



        }
    }

}
