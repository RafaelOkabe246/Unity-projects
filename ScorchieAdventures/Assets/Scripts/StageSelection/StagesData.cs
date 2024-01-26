using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagesData : MonoBehaviour
{
    public static Dictionary<int, Stage[]> stagesData;

    public static void LoadStagesData(int numberOfSlots) 
    {
        stagesData = new Dictionary<int, Stage[]>();

        stagesData.Clear();

        for (int i = 0; i < numberOfSlots; i++) 
        {
            if (SaveSystem.LoadTheGame(i) != null)
                stagesData[i] = SaveSystem.LoadTheGame(i).saveSlot.stageInfo;
            else
            {
                Stage[] stages = new Stage[5];

                for (int j = 0; j < stages.Length; j++) 
                {
                    stages[j].ID = j;
                    stages[j].stageState = StageState.NOT_VISIBLE;
                    if (j == 0)
                        stages[j].stageState = StageState.VISIBLE;
                }

                stagesData[i] = stages;

                Debug.LogWarning("There's no stage data in game slot " + i);
            }
        }
    }
}
