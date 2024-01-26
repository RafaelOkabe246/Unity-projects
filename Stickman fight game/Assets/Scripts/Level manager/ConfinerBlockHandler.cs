using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfinerBlockHandler : MonoBehaviour
{
    public ConfinerBlock currentBlock;
    public ConfinerBlock nextBlock;
    public CameraManager cameraManager;

    public void NextBlockTransition()
    {
        currentBlock.SetBounds();
        cameraManager.CameraBlockTransition(currentBlock.cameraLimits);
        
    }



}
