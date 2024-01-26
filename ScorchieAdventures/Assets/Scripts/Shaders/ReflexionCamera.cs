using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Cinemachine;

public class ReflexionCamera : MonoBehaviour
{
    public Transform cameraPos;
    public RenderTexture reflexionTexture;
    public Material waterSurfaceMaterial;
    public SpriteRenderer waterSprite;
    public Camera _camera;
    public float newCameraSize;
    

    private void Start()
    {
        reflexionTexture = new RenderTexture(256, 128, 16, RenderTextureFormat.ARGB32);
        reflexionTexture.Create();
        reflexionTexture.Release();
        _camera.targetTexture = reflexionTexture;

        waterSurfaceMaterial = GetComponentInChildren<SpriteRenderer>().material;

      

        waterSurfaceMaterial.SetTexture("_ReflexionTex", reflexionTexture);


        SetCameraPos();
    }

    void SetCameraPos()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = waterSprite.transform.localScale.x / waterSprite.transform.localScale.y;

        if (screenRatio >= targetRatio)
        {
            _camera.orthographicSize = waterSprite.transform.localScale.y / 2;
        }
        else
        {
            float differentInSize = targetRatio / screenRatio;
            _camera.orthographicSize = waterSprite.transform.localScale.y / 2 * differentInSize;
        }

        cameraPos.localPosition = new Vector3(cameraPos.localPosition.x, cameraPos.localPosition.y + waterSprite.bounds.size.y, -1f);
        _camera.transform.localPosition = cameraPos.localPosition;
    }


}
