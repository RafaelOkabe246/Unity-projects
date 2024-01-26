using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.RemoteConfig;
using System;

public class FileSystem : MonoBehaviour
{
    public struct userAtributtes { }
    public struct appAtributtes { }

    public int waveBossNumber;
    public string codigoEmblema;
    public static string wordsList;

    [System.Obsolete]
    private void Awake()
    {
        ConfigManager.FetchCompleted += SetTest;
        ConfigManager.FetchConfigs<userAtributtes, appAtributtes>(new userAtributtes(), new appAtributtes());
    }

    [System.Obsolete]
    void SetTest(ConfigResponse response)
    {
        waveBossNumber = ConfigManager.appConfig.GetInt("BossWave");
        codigoEmblema = ConfigManager.appConfig.GetString("CodigoEmblema");
        wordsList = ConfigManager.appConfig.GetString("Lista");
        WordGenerator.wordsCustom = new List<string>(wordsList.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries));
        foreach (string word in WordGenerator.wordsCustom)
        {
            Debug.Log($"{word}");
        }
        WaveSystem.bossWave = waveBossNumber;
    }
    

}
