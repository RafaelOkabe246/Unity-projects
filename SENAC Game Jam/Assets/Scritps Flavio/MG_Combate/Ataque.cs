using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ataque : MonoBehaviour {
    private Animator pata;
    public float cooldown = 1f;
    public int maxTentaticas;
    public int tentativas;
    private float cooldownTimer = 0f;

    public TextMeshProUGUI tentativasText;

    public MinijogoController minijogoController;   //Informa se o jogo acabou
    bool endGame;

    private void Start()
    {
        endGame = false;
        minijogoController = FindObjectOfType<MinijogoController>();

        maxTentaticas = Random.Range(4,7);
        tentativas = maxTentaticas;
        pata = GetComponent<Animator>();
    }

    void Update() {
        tentativasText.text = "Chances para acertar: " + tentativas;

        if (tentativas <= 0 && !endGame)
        {
            endGame = true;
            minijogoController.MinijogoEnded(false);
        }

        if (Input.GetMouseButtonDown(0) && tentativas > 0)
        {
            tentativas -= 1;
            SoundSystem.instance.PlayAudio(AudioReference.enemyHit, AudioType.ENEMY);
            pata.SetTrigger("Atq");
        }
    }
}
