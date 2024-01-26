using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    public int currentScenarioIndex;
    public GameObject[] scenariosObjs;


    private void Awake()
    {
        instance = this;
    }

    #region Scenario_transition

    public void ScenarioTransition(int scenarioIndex)
    {
        for (int i = 0; i < scenariosObjs.Length; i++)
        {
            if(i == scenarioIndex)
            {
                currentScenarioIndex = i;
                scenariosObjs[i].SetActive(true);
            }
            else
            {
                scenariosObjs[i].SetActive(false);
            }
        }
    }


    #endregion

    #region Scene_change
    public void Win()
    {
        SceneManager.LoadScene(1);
    }

    public void Dead()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(0);

    }
    #endregion
}
