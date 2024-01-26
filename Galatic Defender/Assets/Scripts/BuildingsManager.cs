using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsManager : MonoBehaviour
{
    public Base[] Bases;

    public Cidade[] Cidades;

    [SerializeField] private GameSystem GS;

    private void Awake()
    {
        GS = GetComponentInParent<GameSystem>();
    }

}
