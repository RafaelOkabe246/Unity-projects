
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum AudioType { UI_or_Game, PLAYER, ENEMY, BUTTON }

public class SoundSystem : MonoBehaviour
{
    public static SoundSystem instance;

    public AudioSource sfxSource;
    public AudioSource musicSource;
    public AudioEvent[] uiGameAudios;
    public AudioEvent[] playerAudios;
    public AudioEvent[] enemyAudios;
    public AudioEvent[] buttonAudios;

    private void Awake()
    {
        instance = this;
    }


    public void PlayAudio(string audioName, AudioType audioType)//, AudioSource audioSource)
    {
        List<AudioEvent> audiosList = new List<AudioEvent>();

        switch (audioType)
        {
            case AudioType.UI_or_Game:
                AudioEvent uiAudio = Array.Find(uiGameAudios, AudioEvent => AudioEvent.name == audioName);
                audiosList.Add(uiAudio);
                break;
            case AudioType.PLAYER:
                AudioEvent playerAudio = Array.Find(playerAudios, AudioEvent => AudioEvent.name == audioName);
                audiosList.Add(playerAudio);
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

        AudioSource audioSourceToUse = sfxSource;//(audioSource != null) ? audioSource : sfxSource;

        for (int i = 0; i < audiosList.Count; i++)
        {
            if (audiosList[i] != null)
            {
                audioSourceToUse.clip = audiosList[i].clip;
                audioSourceToUse.volume = audiosList[i].volume;
                audioSourceToUse.Play();
            }
        }
    }
}
