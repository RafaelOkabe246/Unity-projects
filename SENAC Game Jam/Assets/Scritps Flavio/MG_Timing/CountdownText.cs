using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownText : MonoBehaviour {
    //  Eu pensei no minigame do parry ter uma temática do tipo: a pata está correndo e atiram uma flecha,
    //  você tem que apertar na tela dentro de um tempo limite para defender,
    //  se for muito cedo você falha e se for muito tarde a flecha te acerta.
    //  Sugestões e alterações são bem-vindas!

    public Slider paryTimerSlider;                  //Indica visualmente o time 
    public GameObject arrowAlertIcon;               //Indica o momento certo para desviar

    public TextMeshProUGUI countdownText;           // gameObject do texto da tela
    
    private bool clicou = false;                    // Pra verificar se o jogador já clicou (do contrário seria só spammar o clique)
    private bool endedMiniGame;                     // Informa se o minigame acabou
    [SerializeField] private float value = 10f;     // "Distância" em que a flecha é disparada 
    [SerializeField] private float limite = 100f;   // Distância mínima para o jogador se defender quando clicar
    
    private float clickTime = 0f;                   // Momento em que o jogador clicou
    private float countdownValue = 0;

    public MinijogoController minijogoController;   //Informa se o jogo acabou


    void Start() {
        minijogoController = FindObjectOfType<MinijogoController>();
        endedMiniGame = false;
        //value = Random.Range(value - 1, value + 1);
        limite = Random.Range(75f, 200f);
        //value *= 10f;
        countdownValue = value;
        paryTimerSlider.maxValue = value;
        UpdateCountdownText();
        UpdateSlide();
    }

    private void Update() {
            UpdateSlide();
        if (Input.GetMouseButtonDown(0) && !clicou && !endedMiniGame) 
        {               //  Se ele ainda não tiver clicado
            clicou = true;
            clickTime = countdownValue * 100;

            if(clickTime > 0)                                       //  E ele clicar antes de "acabar o tempo"
            {
                if (clickTime < limite)                             //  E depois do mínimo
                {
                    Debug.Log("Defendeu! Metros de distância: " + (countdownValue * 100).ToString("F0") + "m");
                    endedMiniGame = true;
                    minijogoController.MinijogoEnded(true);
                    return;
                }
                else                                                //  Do contrário perde ?
                {
                    Debug.Log("Cedo demais! A flecha te acertou! Metros de distância: " + (countdownValue * 100).ToString("F0") + "m");
                    endedMiniGame = true;
                    minijogoController.MinijogoEnded(false);
                    return;
                }
            } 
            else
            {
                Debug.Log("Demorou demais! A flecha te acertou");
                endedMiniGame = true;
                minijogoController.MinijogoEnded(false);
                return;
            }
        }
    }


    private void FixedUpdate() //   Conta o tempo e faz update no texto
    {
        if (countdownValue > 0.1f)
        {
            countdownValue -= Time.deltaTime;
            UpdateCountdownText();
            if (countdownValue * 100 < limite)
                arrowAlertIcon.SetActive(true);
        }
        else if (countdownValue <= 0.1f && !clicou)
        {
            minijogoController.MinijogoEnded(false);
        }
    }


    private void UpdateCountdownText() {    //  Faz update no texto
        countdownText.text = "Metros: " + (countdownValue * 100).ToString("F1");
    }

    private void UpdateSlide()    //Faz o update da barra de pary
    {
        paryTimerSlider.value = countdownValue;
    }
}
