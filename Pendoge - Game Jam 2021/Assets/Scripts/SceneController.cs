using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public const string StartGameScene = "Intro Cutscene Scene";
    public const string GameplayScene = "Gameplay Scene";
    public const string GameOverScene = "Fim de jogo";

    public void StartGameCutscene()
    {
        SceneManager.LoadScene(SceneController.StartGameScene);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(SceneController.GameplayScene);
    }

    public void GameOver()
    {
        SceneManager.LoadScene(SceneController.GameOverScene);
    }
}
