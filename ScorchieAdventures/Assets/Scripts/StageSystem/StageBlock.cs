using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBlock : MonoBehaviour
{
   [SerializeField] private List<GameObject> enemiesAndGimmicks;
    public Transform cameraFocusPoint;
    public Transform startPoint;
    [SerializeField] private Transform gimmicksAndEnemiesContainer;
    public float blockCameraOrthographicSize;

    private void Start()
    {
        enemiesAndGimmicks = new List<GameObject>();
        for (int i = 0; i < gimmicksAndEnemiesContainer.childCount; i++)
        {
            enemiesAndGimmicks.Add(gimmicksAndEnemiesContainer.GetChild(i).gameObject);
        }
    }

    public void EnterStageBlock() 
    {
        foreach(GameObject gm in enemiesAndGimmicks) 
            gm.SetActive(true);
    }

    public void ExitStageBlock() 
    {
        foreach (GameObject gm in enemiesAndGimmicks)
            gm.SetActive(false);
    }

    public void ReloadStageBlock() 
    {
        ExitStageBlock();
        EnterStageBlock();
    }

    private void OnDrawGizmos()
    {
        float verticalHeightSeen = blockCameraOrthographicSize * 2.0f;
        float verticalWidthSeen = verticalHeightSeen * Camera.main.aspect;


        //Camera area
        Gizmos.DrawWireCube(cameraFocusPoint.position, new Vector3(verticalWidthSeen, verticalHeightSeen, 0));
    }
}
