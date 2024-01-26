using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ControlaInterface : MonoBehaviour
{
    private ControlaJogador scriptControlaJogador;
    public Slider SliderVidaJogador;
    public GameObject PainelDeGameOver;
    public Text TextoTempoDeSobrevivencia;


    // Start is called before the first frame update
    void Start()
    {
        scriptControlaJogador = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<ControlaJogador>();
        SliderVidaJogador.maxValue = scriptControlaJogador.statusJogador.VidaInicial;
        SliderVidaJogador.value = scriptControlaJogador.statusJogador.Vida;
        Time.timeScale = 1;
    }



    public void AtualizarSliderVidaJogador()
    {
        SliderVidaJogador.value = scriptControlaJogador.statusJogador.Vida;
    }


    public void GameOver()
    {
        PainelDeGameOver.SetActive(true);
        Time.timeScale = 0;

        int minutos = (int)Time.timeSinceLevelLoad / 60;
        int segundos = (int)Time.timeSinceLevelLoad % 60;
        TextoTempoDeSobrevivencia.text = "Você sobreviveu por " + minutos  + " minutos " + " e " + segundos + " segundos";
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene("Game");
    }
}
