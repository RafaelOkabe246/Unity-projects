using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveSystem 
{
    public static List<GameSlot> gameSlots = new List<GameSlot>();
    public static int currentGameSlotIndex;

    public static void GenerateSaveSlots(int slotsNumber)
    {
        gameSlots.Clear();

        for (int i = 0; i < slotsNumber; i++)
        {
            Debug.Log("Slot gerado");
            GameSlot gameSlot = new GameSlot
            {
                ID = i,
                saveSlot = new SaveSlot()
            };
            gameSlot.saveSlot.slotInfo = new RunInfo();

            gameSlots.Add(gameSlot);

        }
    }

    public static void SaveTheGame(RunInfo _runInfo)
    {
        //BinaryFormatter formatter = new BinaryFormatter();
        //string path = Application.persistentDataPath + "/saveSlot" + gameSlot.ID + ".sav";
        //FileStream stream = new FileStream(path, FileMode.Create);

        //formatter.Serialize(stream, gameSlot);
        //stream.Close();

        //Debug.Log("Game saved in slot " + gameSlot.ID);

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/saveSlot" + ".sav";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(_runInfo);

        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("File saved");
    }

    public static SaveData LoadGame()
    {
        string path = Application.persistentDataPath + "/saveSlot" + ".sav";
        if (File.Exists(path))
        {
            Debug.Log("Game loaded");
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }


    /*
    public static GameSlot LoadTheGame(int slotID)
    {
        Debug.Log("Slot id"+gameSlots.Count);
        if (slotID >= gameSlots.Count)
        {
            Debug.LogWarning("The given slotID surpasses the number of Game Slots in the game");
            return null;
        }

        string path = Application.persistentDataPath + "/saveSlot" + slotID + ".sav";
        if (File.Exists(path))
        {
            Debug.Log("Slot carregado");
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
    }*/

    public static void DeleteGameSave()
    {
        string path = Application.persistentDataPath + "/saveSlot"  + ".sav";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        else
            Debug.LogError("Game slot not found in " + path);
    }

}
