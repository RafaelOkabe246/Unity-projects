using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public PlayerStats playerStats;
    public Player player;
    public Radio radio;

   

    public GameObject blackScreen;

    [Header("Default ui")]
    public TMP_Text anmmoNumber;
    public TMP_Text locationText;

    [Header("Shooter mode")]

    [Header("Radio mode")]
    public GameObject uiRadio;
    public GameObject radioNumberText;
    public TMP_Text[] passwordText;

    public GameObject passwordShow;
    public TMP_Text passwordShowText;

    private void Awake()
    {
        instance = this;
    }


    private void Update()
    {
        UpdateUi();
        CheckGameMode();
    }

    void CheckGameMode()
    {
        switch (player._GameMode)
        {
            case (GameMode.Shooting):
                uiRadio.gameObject.SetActive(false);
                radioNumberText.gameObject.SetActive(false);
                break;
            
            case (GameMode.Radio):
                uiRadio.gameObject.SetActive(true);
                radioNumberText.gameObject.SetActive(true);
                break;
        }
    }

    void UpdateUi()
    {
        locationText.text = "Localização: " + SceneController.instance.scenariosObjs[SceneController.instance.currentScenarioIndex].name;
        anmmoNumber.text = "Munição " + playerStats.ammunitionNumber;

        for (int i = 0; i < passwordText.Length; i++)
        {

            if(radio.hasTyppedNumber[i] == true)
                passwordText[i].color = Color.red;
            else if(radio.hasTyppedNumber[i] == false)
                passwordText[i].color = Color.white;

            passwordText[i].text = "" + radio.senhaDigitada[i];
        }

        if (radio.isCorrect)
        {
            for (int i = 0; i < passwordText.Length; i++)
            {
                passwordText[i].color = Color.green;
            }
        }
    }

    public void PlayerDeath()
    {
        SoundManager.instance._musicSource.mute = true;
        blackScreen.SetActive(true);
    }

    public void OpenClosePassword()
    {
        bool OpenClose = passwordShow.gameObject.activeSelf;

        OpenClose = !OpenClose;

        passwordShow.gameObject.SetActive(OpenClose);
    }


}
