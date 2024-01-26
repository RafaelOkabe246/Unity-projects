using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaJogador : MonoBehaviour, IMatavel
{

    private Vector3 direcao;
    public LayerMask chao;
    public GameObject TextoGameOver;
    public bool Vivo = true;


    private MovimentoJogador meuMovimentoJogador;
    private AnimacaoPersonagem animacaoJogador;
    public Status statusJogador;

    public ControlaInterface ScriptControlaInterface;

    [Header("Audio variables")]
    public AudioClip SomDeDano;

    private void Start()
    {
        statusJogador = GetComponent<Status>();
        animacaoJogador = GetComponent<AnimacaoPersonagem>();
        meuMovimentoJogador = GetComponent<MovimentoJogador>();
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        direcao = new Vector3(x, 0f, z);

        animacaoJogador.Movimentar(direcao.magnitude);

    }

    private void FixedUpdate()
    {
        meuMovimentoJogador.Movimentar(direcao, statusJogador.Velocidade);

        meuMovimentoJogador.RotacaoJogador(chao);
    }

    public void TomarDano(int dano)
    {
        statusJogador.Vida -= dano;
        ScriptControlaInterface.AtualizarSliderVidaJogador();
        ControlaAudio.instancia.PlayOneShot(SomDeDano);
        if (statusJogador.Vida <= 0)
        {
            Morrer();
        }
    }

    public void Morrer()
    {
        ScriptControlaInterface.GameOver();
    }
}
