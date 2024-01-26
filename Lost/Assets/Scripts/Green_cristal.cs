using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Green_cristal : MonoBehaviour
{
    public GameObject Explosion;

    public bool PlayerTouch;

    void Start()
    {
        Explosion.GetComponent<AreaEffector2D>().forceMagnitude = 0; 
    }

     void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Explosion.GetComponent<AreaEffector2D>().forceMagnitude = 30;
        }
    }




    void Destroy_and_respawn()
    {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.W))
        {
            PlayerTouch = true;
        }
    }

}
