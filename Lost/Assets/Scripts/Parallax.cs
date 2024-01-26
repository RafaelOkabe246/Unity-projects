using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform Background;
    public float speed;

    private Transform cam;

    private Vector3 previewCamPosition;

    void Start()
    {
        cam = Camera.main.transform;
        previewCamPosition = cam.position;

    }

    void LateUpdate()
    {
        //float paralaxY = previewCamPosition.y - cam.position.y;
        //float bgTargetY = Background.postion.y + paralaxY;

        float paralaxX = previewCamPosition.x - cam.position.x;
        float bgTargetX = Background.position.x + paralaxX;

        Vector3 bgPosition = new Vector3(bgTargetX, Background.position.y, Background.position.z);
        Background.position = Vector3.Lerp(Background.position, bgPosition, speed * Time.deltaTime);

        previewCamPosition = cam.position;
    }
}
