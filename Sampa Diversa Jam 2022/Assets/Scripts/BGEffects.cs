using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using System.Collections;
using System.Collections.Generic;

public class BGEffects : MonoBehaviour
{
    public AudioSource Track1;

    public static float volumeTrack1;
    [HideInInspector]
    public float initialVolumeTrack1 = 0.5f;

    public AudioSource Track2;

    public static float volumeTrack2;
    [HideInInspector]
    public float initialVolumeTrack2 = 0.0f;

    //public static float countdown;
    //[HideInInspector]
    //public float initialCountdown = 5f;

    public Light2D light2D;

    public static float colorRGB;
    [HideInInspector]
    public float initialColorRGB = 0.5f;


    void Start()
    {
        volumeTrack1 = initialVolumeTrack1;
        volumeTrack2 = initialVolumeTrack2;
        //countdown = initialCountdown;

        colorRGB = initialColorRGB;
        light2D = GetComponent<Light2D>();
    }

    /*void Update()
    /{
        countdown -= Time.deltaTime; //countdown também não vai mais usar
        countdown = Mathf.Clamp(countdown,0f,Mathf.Infinity);
        Debug.Log(string.Format("{0:0}", countdown));

        if(countdown <= 0f)
        {
            IncreaseDark();
            //IncreaseLight();
            countdown = initialCountdown;
        }
    }*/

    
    public void IncreaseDark(float IncreaseLevel) 
    {
        float a1 = 0.5f + (IncreaseLevel / 10);
        float a2 = 0.5f - (IncreaseLevel / 10);

        Track1.volume = a2; //Track1.volume - 0.1f;
        Track2.volume = a1;//Track2.volume + 0.1f;

        //colorRGB -= 0.1f;
        light2D.color = new Color(colorRGB, colorRGB, colorRGB);
    }

    /*public void IncreaseLight() //não vai mais usar
    {
        Track1.volume += 0.1f;
        Track2.volume -= 0.1f;

        colorRGB += 0.1f;
        light2D.color = new Color(colorRGB,colorRGB,colorRGB);
    }*/

    public void CheckShadowLight(int LuzSombra)
    {

        if(LuzSombra != 0)
        {
            IncreaseDark(LuzSombra);
        }
    }
}
