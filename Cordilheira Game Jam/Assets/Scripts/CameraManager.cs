using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private BoxCollider2D cameraBox; //The box collider of the camera
    [SerializeField]
    private Transform player; //Player position
    private SceneController sceneController;


    void Start()
    {
        sceneController = FindObjectOfType(typeof(SceneController)) as SceneController;
        cameraBox = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Transform>();
    }

    void Update()
    {
        FollowPlayer();
    }

    void AspectRatioChange() //In this case, the camera will adapt to thsese dimensions 
    {
        //16:10 ratio

    }
    void FollowPlayer()
    {
        if (GameObject.Find(Tags.Room_area))
        {
            transform.position = new Vector3(Mathf.Clamp(player.position.x, GameObject.Find(Tags.Room_area).GetComponent<BoxCollider2D>().bounds.min.x + cameraBox.size.x / 2, GameObject.Find(Tags.Room_area).GetComponent<BoxCollider2D>().bounds.max.x - cameraBox.size.x / 2),
                                              Mathf.Clamp(player.position.y, GameObject.Find(Tags.Room_area).GetComponent<BoxCollider2D>().bounds.min.y + cameraBox.size.y / 2, GameObject.Find(Tags.Room_area).GetComponent<BoxCollider2D>().bounds.max.y - cameraBox.size.y / 2),
                                              transform.position.z);
        }
    }

    public void Morrer()
    {
        //sceneController.Morreu();
    }

}
