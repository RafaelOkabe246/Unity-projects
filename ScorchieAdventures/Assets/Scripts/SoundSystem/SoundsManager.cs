
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum AudioType {UI, PLAYER, COLLECTABLE, ENEMY, BUTTON }

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager instance;

    public AudioSource sfxSource;
    public AudioSource secondSfxSource;
    public AudioSource musicSource;
    public AudioEvent[] uiAudios;
    public AudioEvent[] playerAudios;
    public AudioEvent[] colectableAudios;
    public AudioEvent[] enemyAudios;
    public AudioEvent[] buttonAudios;

    private void Awake()
    {
        instance = this;
    }

    public void PlayAudio(string audioName, AudioType audioType, AudioSource audioSource)
    {
        List<AudioEvent> audiosList = new List<AudioEvent>();

        switch (audioType) 
        {
            case AudioType.UI:
                AudioEvent uiAudio = Array.Find(uiAudios, AudioEvent => AudioEvent.name == audioName);
                audiosList.Add(uiAudio);
                break;
            case AudioType.PLAYER:
                AudioEvent playerAudio = Array.Find(playerAudios, AudioEvent => AudioEvent.name == audioName);
                audiosList.Add(playerAudio);
                break;
            case AudioType.COLLECTABLE:
                AudioEvent colectableAudio = Array.Find(colectableAudios, AudioEvent => AudioEvent.name == audioName);
                audiosList.Add(colectableAudio);
                break;
            case AudioType.ENEMY:
                AudioEvent enemyAudio = Array.Find(enemyAudios, AudioEvent => AudioEvent.name == audioName);
                audiosList.Add(enemyAudio);
                break;
            case AudioType.BUTTON:
                AudioEvent buttonAudio = Array.Find(buttonAudios, AudioEvent => AudioEvent.name == audioName);
                audiosList.Add(buttonAudio);
                break;
        }

        AudioSource audioSourceToUse = (audioSource != null) ? audioSource : sfxSource;

        for (int i = 0; i < audiosList.Count; i++)
        {
            if (audiosList[i] != null)
            {
                audioSourceToUse.clip = audiosList[i].clip;
                audioSourceToUse.Play();
            }
        }
    }

    public void PlayMusic()
    {

    }

    public void SetNewMusic()
    {
        
    }
}
