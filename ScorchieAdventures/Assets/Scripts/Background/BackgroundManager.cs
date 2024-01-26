using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    private Camera parentCamera;
    private CameraController cameraController;
    public SpriteRenderer layer0;

    private void Start()
    {
        parentCamera = GetComponentInParent<Camera>();
    }

    private void Update()
    {
        SetBackgroundSize();
    }

    void SetBackgroundSize()
    {
        float cameraHeight = 2f * parentCamera.orthographicSize;
        float cameraWidth = cameraHeight * parentCamera.aspect;

        float layer0_spriteHeight = layer0.sprite.bounds.size.y;
        float layer0_spriteWidth = layer0.sprite.bounds.size.x;

        Vector3 scale = transform.localScale;
        scale.x = cameraWidth / layer0_spriteWidth;
        scale.y = cameraHeight / layer0_spriteHeight;

        transform.localScale = scale;
    }

}
