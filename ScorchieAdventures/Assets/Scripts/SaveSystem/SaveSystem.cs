using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static List<GameSlot> gameSlots = new List<GameSlot>();
    public static int currentGameSlot;

    public static void GenerateGameSlots(int numberOfSlots) 
    {
        gameSlots.Clear();
        for (int i = 0; i < numberOfSlots; i++) 
        {
            GameSlot gameSlot = new GameSlot
            {
                ID = i,
                saveSlot = new SaveSlot()
            };
            gameSlot.saveSlot.stageInfo = new Stage[5];
            for (int j = 0; j < gameSlot.saveSlot.stageInfo.Length; j++) 
            {
                gameSlot.saveSlot.stageInfo[j] = new Stage();
            }
            gameSlots.Add(gameSlot);

            /*BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/saveSlot" + gameSlot.ID + ".sav";
            FileStream stream = new FileStream(path, FileMode.Create);
            formatter.Serialize(stream, gameSlot);
            stream.Close();*/
        }
        Debug.Log(gameSlots.Count + " game slots were created!");
    }

    public static void SelectGameSlot(int slotToSelect) 
    {
        if (slotToSelect < gameSlots.Count && slotToSelect >= 0)
            currentGameSlot = slotToSelect;
    }

    public static void SaveTheGame(GameSlot gameSlot) 
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/saveSlot" + gameSlot.ID + ".sav";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, gameSlot);
        stream.Close();

        Debug.Log("Game saved in slot " + gameSlot.ID);
    }

    public static GameSlot LoadTheGame(int slotID) 
    {
        if (slotID >= gameSlots.Count)
        {
            Debug.LogWarning("The given slotID surpasses the number of Game Slots in the game");
            return null;
        }

        string path = Application.persistentDataPath + "/saveSlot" + slotID + ".sav";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            if (stream != null)
            {
                gameSlots[slotID] = formatter.Deserialize(stream) as GameSlot;
                stream.Close();

                return gameSlots[slotID];
            }
            return null;
        }
        else 
        {
            Debug.LogWarning("Game slot not found in " + path);
            return null;
        }
    }

    public static void DeleteGameSlot(int slotID) 
    {
        string path = Application.persistentDataPath + "/saveSlot" + slotID + ".sav";
        if (File.Exists(path))
        {
            File.Delete(path);
            gameSlots[slotID] = new GameSlot();
            gameSlots[slotID].saveSlot = new SaveSlot();
            gameSlots[slotID].saveSlot.stageInfo = new Stage[5];
            for (int j = 0; j < gameSlots[slotID].saveSlot.stageInfo.Length; j++)
            {
                gameSlots[slotID].saveSlot.stageInfo[j] = new Stage();
            }
        }
        else
            Debug.LogError("Game slot not found in " + path);
    }

    public static void StoreDataInGameSlot(int slotID, SaveSlot saveSlot) 
    {
        gameSlots[slotID].saveSlot = new SaveSlot(saveSlot.stageInfo);
    }
}
