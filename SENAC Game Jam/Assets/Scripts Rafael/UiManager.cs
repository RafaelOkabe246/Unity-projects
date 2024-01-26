using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    public Animator MinigameBonusPanel;
    public GameObject minigamePreviewPanel;
    public GameObject dadosButton;

    public SlotManager slotManager;
    public TextMeshProUGUI resultadoDoDadoText;

    public TextMeshProUGUI resultadoDoMinigameText;
    public Image miniJogoPreview;
    public Sprite[] miniJogosPreviews;

    public int miniGameSceneIndex;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ShowResultadoDoMinijogo();
    }

    public void ShowMinigamePreview()
    {
        minigamePreviewPanel.SetActive(true);
        miniGameSceneIndex = RandomMinijogoSceneIndex();
    }

    public void OpenMinijogo()
    {
        SceneManagement.instance.LoadLevel(miniGameSceneIndex);
    }

    public void ShowResultadoDoMinijogo()
    {
        if (SlotManager.turnosGansoEspera > 0)
        {
            resultadoDoDadoText.gameObject.SetActive(true);
            if (GameManager.instance.hasWonMinigame)
                resultadoDoMinigameText.text = "Você ganhou o minijogo!";
            else if (!GameManager.instance.hasWonMinigame)
                resultadoDoMinigameText.text = "Você perdeu o minijogo!";
        }
    }


    public void ShowResultadoDosDados(int i)
    {
        
        if (GameManager.instance.hasWonMinigame)
            resultadoDoDadoText.text = "Você tirou " + i + " no dado!";
        else if (!GameManager.instance.hasWonMinigame)
            resultadoDoDadoText.text = "Você não irá se mover pois perdeu o minijogo passado";
        
            
    }

    int RandomMinijogoSceneIndex()
    {
        int _random = UnityEngine.Random.Range(SceneManagement.endlessRunnerSceneIndex, SceneManagement.combatSceneIndex + 1);

        switch (_random)
        {
            case (SceneManagement.endlessRunnerSceneIndex):
                miniJogoPreview.sprite = miniJogosPreviews[0];
                break;
            case (SceneManagement.parySceneIndex):
                miniJogoPreview.sprite = miniJogosPreviews[1];
                break;
            case (SceneManagement.combatSceneIndex):
                miniJogoPreview.sprite = miniJogosPreviews[2];
                break;
        }
        
        return _random;
    }

    public void ShowMiniGameResults()
    {
        if (MinigameBonusPanel == null)
            MinigameBonusPanel = GameObject.Find("MinigameResults-Panel").GetComponent<Animator>();
        MinigameBonusPanel.SetTrigger("ShowResults");
    }
}
