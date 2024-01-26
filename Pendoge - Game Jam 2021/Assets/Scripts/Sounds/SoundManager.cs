using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public MainGameController _MainGameController;

    public AudioSource SFXSound;
    public AudioSource MusicSource;

    public float Volume;

    public void PlaySFX(AudioClip audioClip)
    {
        SFXSound.PlayOneShot(audioClip, Volume);
    }


}
