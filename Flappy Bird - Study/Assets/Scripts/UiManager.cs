using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI counterText;
    public TextMeshProUGUI highscoreText;
    public TextMeshProUGUI pointsText; 
    public GameObject tutorialText;
    public GameObject pauseCanvas, gameplayCanvas, gameOverCanvas, starterCanvas;

    private void Start()
    {
        UpdatePoints();
        highscoreText.text = "Recorde: "+ PlayerPrefs.GetInt("Highscore", 0).ToString();
    }

    void UpdateHighScore()
    {
        if(GameManager.instance.points > PlayerPrefs.GetInt("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", GameManager.instance.points);
            highscoreText.text = "Recorde: " + PlayerPrefs.GetInt("Highscore", 0).ToString();
        }
    }

    public void UpdatePoints()
    {
        pointsText.text = "Pontos:" + GameManager.instance.points.ToString();
        UpdateHighScore();
    }

    public void ChangeCanvas(GameState currentGameState)
    {
        switch (currentGameState)
        {
            case (GameState.Lose):
                gameOverCanvas.SetActive(true);
                gameplayCanvas.SetActive(false);
                pauseCanvas.SetActive(false);
                starterCanvas.SetActive(false);


                break;

            case (GameState.Gameplay):
                gameOverCanvas.SetActive(false);
                gameplayCanvas.SetActive(true);
                pauseCanvas.SetActive(false);
                starterCanvas.SetActive(false);


                Time.timeScale = 1;

                break;

            case (GameState.Paused):
                gameOverCanvas.SetActive(false);
                gameplayCanvas.SetActive(false);
                pauseCanvas.SetActive(true);
                starterCanvas.SetActive(false);

                Time.timeScale = 0;

                break;
            case (GameState.Start):
                gameOverCanvas.SetActive(false);
                gameplayCanvas.SetActive(false);
                pauseCanvas.SetActive(false);
                starterCanvas.SetActive(true);


                break;
        }
    }
}
