using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private AudioController _AudioController;

    private int CenaAtual;

    private void Start()
    {
        CenaAtual = SceneManager.GetActiveScene().buildIndex;
        _AudioController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioController>();

    }

    

    public void Quit()
    {
        Application.Quit();
    }

    public void Voltar()
    {
        SceneManager.LoadScene(Player.CenaAtual);
        _AudioController.Change_music();
    }

    public void Start_the_game()
    {
        SceneManager.LoadScene("Fase 1");
        _AudioController.Change_music();
    }
    //Start screen

    public void Start_Opening()
    {
        SceneManager.LoadScene("Start screen");
    }

    public void Morreu()
    {
        SceneManager.LoadScene("Dead screen");

    }

    public void Next_level()
    {
        SceneManager.LoadScene(CenaAtual + 1);
        _AudioController.Change_music();
    }

    public void OpenMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Next_level_whithout_music()
    {
        SceneManager.LoadScene(CenaAtual + 1);
    }
}
