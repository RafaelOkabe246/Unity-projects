using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SaveSlot
{
    public Stage[] stageInfo;

    public SaveSlot(Stage[] stages)
    {
        stageInfo = stages;
    }
}

[System.Serializable]
public class GameSlot
{
    public int ID;
    public SaveSlot saveSlot;
}
