using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public Scene[] Scenes;
    private int sceneIndex;

    internal static object GetActiveScene()
    {
        throw new NotImplementedException();
    }

    internal static void LoadScene(object p)
    {
        throw new NotImplementedException();
    }

    public static void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void SelectLevel(int index)
    {
        SceneManager.LoadScene(Scenes[index].buildIndex);
    }
}
