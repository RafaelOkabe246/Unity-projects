using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Luminosidade
{
    Louis,
    Normal,
};

public class LightController : MonoBehaviour
{
    [SerializeField] internal Players_Controller Controller;

    public Luminosidade Luminosidade_atual;

    [SerializeField] internal bool Louis_is_selected;
    [SerializeField] internal GameObject Dark_vision;

    private Camera Cam;

    private void Start()
    {
        Cam = Camera.main;
    }


    private void Update()
    {
        if (Controller.Personagem_sendo_controlado == Personagem.Louis)
        {
          //  Dark_vision.SetActive(true);
        }
        else if (Controller.Personagem_sendo_controlado != Personagem.Louis)
        {
          //  Dark_vision.SetActive(false);
        }
    }
}
