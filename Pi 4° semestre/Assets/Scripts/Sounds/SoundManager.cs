
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum AudioType { UI, CHARACTER, COLLECTABLE, OBJECT, MUSIC }

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    
    public AudioSource sfxSource;
    public AudioSource secondSfxSource;
    public AudioSource musicSource;

    [Space(5f)]

    public AudiosContainer uiAudiosContainer;
    public AudiosContainer characterAudiosContainer;
    public AudiosContainer objectsAudiosContainer;
    public AudiosContainer colectableAudiosContainer;
    public AudiosContainer musicAudiosContainer;

    private void Awake()
    {
        instance = this;
    }

    public void PlayAudio(string audioName, AudioType audioType, AudioSource audioSource)
    {
        List<AudioEvent> audiosList = new List<AudioEvent>();

        AudiosContainer audioContainer = uiAudiosContainer;

        switch (audioType)
        {
            case AudioType.UI:
                audioContainer = uiAudiosContainer;
                break;
            case AudioType.CHARACTER:
                audioContainer = characterAudiosContainer;
                break;
            case AudioType.COLLECTABLE:
                audioContainer = colectableAudiosContainer;
                break;
            case AudioType.OBJECT:
                audioContainer = objectsAudiosContainer;
                break;
            case AudioType.MUSIC:
                audioContainer = musicAudiosContainer;
                break;
            default:
                audioContainer = uiAudiosContainer;
                break;
        }

        AudioEvent audio = Array.Find(audioContainer.audios, AudioEvent => AudioEvent.audioEventName == audioName);
        audiosList.Add(audio);

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
    
    public void PlayMusic(string audioName, AudioSource audioSource)
    {
        AudiosContainer audioContainer = musicAudiosContainer;

        AudioSource audioSourceToUse = (audioSource != null) ? audioSource : musicSource;

        AudioEvent audio = Array.Find(audioContainer.audios, AudioEvent => AudioEvent.audioEventName == audioName);

        audioSourceToUse.clip = audio.clip;
        audioSourceToUse.Play();
    }

    public void SetNewMusic()
    {

    }
}
