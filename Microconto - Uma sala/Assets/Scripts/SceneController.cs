using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController : MonoBehaviour
{


    public void ChangeSceneHouse()
    {
        //Carregar a cena classificada
        SceneManager.LoadScene("House Scene");
    }
    public void ChangeSceneForest()
    {
        //Carregar a cena classificada
        SceneManager.LoadScene("Forest Scene");
    }
}
