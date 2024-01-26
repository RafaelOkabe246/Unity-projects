using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SavedLevels : MonoBehaviour
{
    public LevelsStorage levelsStorage;

    public LevelSlot levelSlotPrefab;

    public RectTransform parenterer;


    void GenerateLevelsSlots()
    {
        //SaveData saveData = SaveSystem.LoadGame();

        /*/int levelsPassed = saveData.saveInfo.levelsPassed;

        for (int i = 0; i < levelsPassed; i++)
        {
            LevelSlot newLevelSlot = Instantiate(levelSlotPrefab, parenterer);
            newLevelSlot.SetLevel(levelsStorage.gameLevels[i]);
            newLevelSlot.SetLevelUI();
        }
        */
    }
}
