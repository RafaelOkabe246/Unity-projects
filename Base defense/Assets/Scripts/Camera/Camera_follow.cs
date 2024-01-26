using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_follow : MonoBehaviour
{
    private BoxCollider2D cameraBox; //The box collider of the camera

    public float speed;

    private Camera cam;
    private float zoom;
    private float zoomFactor = 3f;
    [SerializeField] private float zoom_speed = 10f;

    void Start()
    {
        cam = Camera.main;

        cameraBox = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        ScrollMouse();
        CameraBounds();
        Move(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));

    }

    
    void CameraBounds()
    {

        if (GameObject.Find("Camera area"))
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, GameObject.Find("Camera area").GetComponent<BoxCollider2D>().bounds.min.x + cameraBox.size.x / 2, GameObject.Find("Camera area").GetComponent<BoxCollider2D>().bounds.max.x - cameraBox.size.x / 2),
                                              Mathf.Clamp(transform.position.y, GameObject.Find("Camera area").GetComponent<BoxCollider2D>().bounds.min.y + cameraBox.size.y / 2, GameObject.Find("Camera area").GetComponent<BoxCollider2D>().bounds.max.y - cameraBox.size.y / 2),
                                              transform.position.z);
        }

    }

    void Move(Vector2 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }


    private void ScrollMouse()
    {
        float scroll_value;
        scroll_value = Input.GetAxis("Mouse ScrollWheel");

        zoom -= scroll_value * zoomFactor;
        zoom = Mathf.Clamp(zoom, 4.5f, 8f);

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoom, Time.deltaTime * zoom_speed);
    }
}
