using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentStage : MonoBehaviour
{
    public static CurrentStage instance;

    public Stage currentStageInfo;
    public int ID;

    private void Start()
    {
        instance = this;
        currentStageInfo = new Stage();
    }
}
