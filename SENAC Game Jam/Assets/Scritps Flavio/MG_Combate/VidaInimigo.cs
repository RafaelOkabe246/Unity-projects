using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidaInimigo : MonoBehaviour {
    public int vidaMax = 3;
    public int vidaAtual;

    public Slider paryTimerSlider;                  //Indica visualmente a vida do inimigo 
    public bool hasDied;
    public MinijogoController minijogoController;   //Informa se o jogo acabou

    void Start() {
        hasDied = false;
        paryTimerSlider.maxValue = vidaMax;
        vidaAtual = vidaMax;
        minijogoController = FindObjectOfType<MinijogoController>();
    }

    private void Update()
    {
        paryTimerSlider.value = vidaAtual;

        if (vidaAtual <= 0 && !hasDied)
        {
            hasDied = true;

            minijogoController.MinijogoEnded(true);
        }
    }

}
