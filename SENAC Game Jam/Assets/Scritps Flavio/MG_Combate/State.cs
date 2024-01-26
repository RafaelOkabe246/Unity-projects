using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour {
    private int random;

    public enum StateType {
        Protegido,
        Vulneravel
    }

    public StateType estado;

    private void Start() {
        estado = StateType.Protegido;
        random = 3;
        Invoke(nameof(ChangeTime), 1f);
    }

    private void ChangeState() {
        Debug.Log("Mudei " + random);

        if(estado == StateType.Protegido)
        {
            estado = StateType.Vulneravel;
            random = Random.Range(4, 6);
        }
        else
        {
            estado = StateType.Protegido;
            random = Random.Range(4, 6);
        }
    }

    private void ChangeTime()
    {
        InvokeRepeating(nameof(ChangeState), 1, random);
    }
}
