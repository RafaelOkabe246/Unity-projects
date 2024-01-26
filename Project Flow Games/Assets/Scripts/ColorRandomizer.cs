using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRandomizer : MonoBehaviour
{
    public Color[] colors;
    private SpriteRenderer spr;
    //private Material material;

    void Start()
    {
        //material = GetComponent<Renderer>().material;
        spr = GetComponent<SpriteRenderer>();
        RandomizeColor();
    }

    public void RandomizeColor() {
        spr.color = colors[Random.Range(0, colors.Length)];
        //material.SetColor("_Color", colors[Random.Range(0, colors.Length)]);
    }
}
