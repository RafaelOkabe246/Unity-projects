using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int lights;
    [HideInInspector]
    public int initialLights=0;
    private Player2DControl Player;
    public Text lightsText;

    void Start()
    {
        Player = GetComponent<Player2DControl>();
        lights = initialLights;
        Player.LuzesSombras = lights;

    }

    private void Update()
    {
        lights = Player.LuzesSombras;
        lightsText.text = lights.ToString();
    }
}
