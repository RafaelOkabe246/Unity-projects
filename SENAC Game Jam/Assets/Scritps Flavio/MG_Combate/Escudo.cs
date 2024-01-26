using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escudo : MonoBehaviour {
    public GameObject escudo;
    private State state;

    private void Start()
    {
        state = GetComponent<State>();
    }

    private void Update() {
        if (state.estado == State.StateType.Vulneravel)
        {
            escudo.SetActive(false);
        } else
        {
            escudo.SetActive(true);
        }
    }
}
