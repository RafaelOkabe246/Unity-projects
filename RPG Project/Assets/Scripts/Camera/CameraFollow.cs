using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private BoxCollider2D cameraBox; //The box collider of the camera
    [SerializeField]
    private Transform player; //Player position

    void Start()
    {
        cameraBox = this.GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();


    }

    void Update()
    {
        //AspectRatioChange();
        FollowPlayer();
    }

    void AspectRatioChange() //In this case, the camera will adapt to thsese dimensions 
    {
        //16:10 ratio
        
    }

    public void FollowPlayer()
    {
        if (GameObject.FindGameObjectWithTag("Room")){
            transform.position = new Vector3 (Mathf.Clamp(player.position.x, GameObject.Find("Room").GetComponent<BoxCollider2D>().bounds.min.x + cameraBox.size.x / 2, GameObject.Find("Room").GetComponent<BoxCollider2D>().bounds.max.x - cameraBox.size.x / 2),
                                              Mathf.Clamp(player.position.y, GameObject.Find("Room").GetComponent<BoxCollider2D>().bounds.min.y + cameraBox.size.y / 2, GameObject.Find("Room").GetComponent<BoxCollider2D>().bounds.max.y - cameraBox.size.y / 2),
                                              transform.position.z);

        }

    }

    public void Activate()
    {
        cameraBox.enabled = true;
        
    }
}
