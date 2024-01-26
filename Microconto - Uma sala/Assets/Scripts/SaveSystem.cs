using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(CharacterEventSystem player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.events";
        FileStream stream = new FileStream(path, FileMode.Create);

        EventManager data = new EventManager(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static EventManager LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.events";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            EventManager data = formatter.Deserialize(stream) as EventManager;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }

    }
}
