using System.Collections;
using UnityEngine;
using Cinemachine;
using System;
using System.Collections.Generic;
/*
* Responsible for blending the camera position, stopping the time during the transition, enabling the next block and disabling the previous block
*/

public class StageBlocksHandler : MonoBehaviour
{
    
    private CinemachineVirtualCamera vCam;

    [SerializeField] private StageBlock currentBlock;
    public static StageBlock savedCurrentBlock;

    public static Action<StageBlock> OnStartBlocksTransition;

    public delegate void StageBlockHandler();
    public static event StageBlockHandler OnStageBlockTransitionStarted;
    public static event StageBlockHandler OnStageBlockTransitionEnded;

    public static bool isVerticalTransition;
    public static Transform previousPos;
    public static Transform nextPos;

    [HideInInspector] public GameObject playerObj;

    [Header("ItemsCollected")]
    public static List<CollectableCoin> collectedFruitsInBlock;
    public static List<CollectableCrystal> collectedCrystalsInBlock;

    private void Awake()
    {
        if(savedCurrentBlock == null)
            savedCurrentBlock = currentBlock;

        collectedFruitsInBlock = new List<CollectableCoin>();
        collectedCrystalsInBlock = new List<CollectableCrystal>();
    }

    private void Start()
    {
        vCam = CameraController.instance.virtualCamera;

        SetCameraInitialFocusPoint(savedCurrentBlock.cameraFocusPoint);
    }

    private void OnEnable()
    {
        OnStartBlocksTransition += StartBlocksTransition;
    }

    private void OnDisable()
    {
        OnStartBlocksTransition -= StartBlocksTransition;
    }

    private void StartBlocksTransition(StageBlock nextBlock)
    {
        currentBlock.ExitStageBlock();
        nextBlock.EnterStageBlock();

        currentBlock = nextBlock;
        savedCurrentBlock = currentBlock;

        OnStageBlockTransitionStarted?.Invoke();

        StartCoroutine(BlendCameraPosition(currentBlock.cameraFocusPoint));

        DestroyCollectedFruits();
        DestroyCollectedCrystals();
    }

    private void DestroyCollectedFruits() 
    {
        foreach (CollectableCoin fruit in collectedFruitsInBlock) 
        {
            Destroy(fruit.gameObject);
        }
        collectedFruitsInBlock.Clear();
    }

    public static void RestoreCollectedFruits() 
    {
        foreach (CollectableCoin fruit in collectedFruitsInBlock)
        {
            fruit.gameObject.SetActive(true);
            fruit.coinsManager.CollectCoin(-1);
        }
        collectedFruitsInBlock.Clear();
    }

    private void DestroyCollectedCrystals()
    {
        foreach (CollectableCrystal crystal in collectedCrystalsInBlock)
        {
            Destroy(crystal.gameObject);
        }
        collectedCrystalsInBlock.Clear();
    }

    public static void RestoreCollectedCrystals()
    {
        foreach (CollectableCrystal crystal in collectedCrystalsInBlock)
        {
            crystal.gameObject.SetActive(true);
            crystal.crystalsManager.CollectCoin(-1);
        }
        collectedFruitsInBlock.Clear();
    }

    private IEnumerator BlendCameraPosition(Transform focusPoint)
    {
        GameStateManager.Instance.SetState(GameState.Paused);

        vCam.Follow = focusPoint;
               
        float newOrthoSize = currentBlock.blockCameraOrthographicSize;
        float elapsedTime = 0f;
        float transitionTime = 1f;
        
        while(elapsedTime < transitionTime)
        {
            //Move player and disable gravity
            playerObj.GetComponent<Rigidbody2D>().gravityScale = 0;
            playerObj.transform.position = Vector3.Lerp(playerObj.transform.position, NextBlockSpawnPoint(), Mathf.SmoothStep(0, 1, elapsedTime/transitionTime));
            //Move camera
            vCam.m_Lens.OrthographicSize = Mathf.Lerp(vCam.m_Lens.OrthographicSize, newOrthoSize, Mathf.SmoothStep( 0f,1f,(elapsedTime/transitionTime)));;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        vCam.m_Lens.OrthographicSize = currentBlock.blockCameraOrthographicSize;

        yield return new WaitForSeconds(1f);
        playerObj.GetComponent<Rigidbody2D>().gravityScale = 1;

        GameStateManager.Instance.SetState(GameState.Gameplay);
        OnStageBlockTransitionEnded?.Invoke();
    }

    private void SetCameraInitialFocusPoint(Transform focusPoint) 
    {
        vCam.m_Lens.OrthographicSize = currentBlock.blockCameraOrthographicSize;
        vCam.Follow = focusPoint;
        vCam.ForceCameraPosition(focusPoint.position, Quaternion.identity);
    }
    
    private Vector3 NextBlockSpawnPoint()
    {
        if(isVerticalTransition)
            return new Vector3(nextPos.position.x, nextPos.position.y, nextPos.position.z);
        else
            return new Vector3(nextPos.position.x, playerObj.transform.position.y, nextPos.position.z);
    }


}
