using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int Modes;

    private SpriteRenderer Fantasia;

    private Game_Controller _GameController;


    void Start()
    {
        _GameController = FindObjectOfType(typeof(Game_Controller)) as Game_Controller;

    }

    void Update()
    {


    }


    void Mode1()
    {

    }


}
