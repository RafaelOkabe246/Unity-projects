using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private BoxCollider2D cameraBox; //The box collider of the camera
    [SerializeField]
    public Transform player; //Player position
    private SceneController sceneController;

    void Start()
    {
        sceneController = FindObjectOfType(typeof(SceneController)) as SceneController;
        cameraBox = GetComponent<BoxCollider2D>();
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
        if (GameObject.Find("Room area"))
        {
            transform.position = new Vector3(Mathf.Clamp(player.position.x, GameObject.Find("Room area").GetComponent<BoxCollider2D>().bounds.min.x + cameraBox.size.x / 2, GameObject.Find("Room area").GetComponent<BoxCollider2D>().bounds.max.x - cameraBox.size.x / 2),
                                              Mathf.Clamp(player.position.y, GameObject.Find("Room area").GetComponent<BoxCollider2D>().bounds.min.y + cameraBox.size.y / 2, GameObject.Find("Room area").GetComponent<BoxCollider2D>().bounds.max.y - cameraBox.size.y / 2),
                                              transform.position.z);

        }
    }
}
