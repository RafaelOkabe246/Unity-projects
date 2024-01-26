using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{

    [SerializeField] private Camera mainCamera;
    public ConfinerBlockHandler confinerBlockHandler;
    public CinemachineConfiner2D cinemachineConfiner;
    [SerializeField] private GameObject leftBound, rightBound;

    public void CameraBlockTransition(PolygonCollider2D newConfiner)
    {
        cinemachineConfiner.InvalidateCache();
        cinemachineConfiner.m_BoundingShape2D = newConfiner;
    }



}
