using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dano : MonoBehaviour
{
    public VidaInimigo vi;
    public Animator anim;
    public State state;

    public void ReduzVida()
    {
        if (state.estado == State.StateType.Vulneravel)
        {
            vi.vidaAtual--;
            anim.SetTrigger("Atq");
            state.estado = State.StateType.Protegido;
        }
    }
}
