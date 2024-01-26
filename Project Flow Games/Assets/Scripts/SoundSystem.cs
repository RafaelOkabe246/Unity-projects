using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    public static SoundSystem instance;

    public AudioSource sfxSource;
    public AudioSource musicSource;

    public AudioClip music;
    public AudioClip[] enemySounds;
    public AudioClip[] typeSounds;
    public AudioClip[] interactiveSounds;

    private void Start()
    {
        musicSource.clip = music;
        musicSource.loop = true;
        musicSource.Play();
    }

    private void Awake()
    {
        instance = this;
    }


    public void SearchSound(int soundIndex, string soundName) // 0 = enemy effect sounds // 1 = type sounds // 2 = interactive sounds
    {
        switch (soundIndex)
        {
            case (0): // 0 = enemy effect sounds
                for (int i = 0; i < enemySounds.Length; i++)
                {
                    if (enemySounds[i].name == soundName)
                    {
                        PlayEnemySounds(enemySounds[i]);
                    }
                }
                break;
            
            case (1): // 1 = type sounds

                PlayTypeSounds();    
                break;
            
            case (2): // 2 = interactive

                PlayInteractiveSounds();
                break;
        }
    }

    private void PlayEnemySounds(AudioClip audioClip)
    {
        sfxSource.PlayOneShot(audioClip);
    }

    private void PlayTypeSounds()
    {
        int soundIndex = Random.Range(0, typeSounds.Length - 1);
        sfxSource.PlayOneShot(typeSounds[soundIndex]);
    }
    private void PlayInteractiveSounds()
    {
        int soundIndex = Random.Range(0, interactiveSounds.Length - 1);
        sfxSource.PlayOneShot(interactiveSounds[soundIndex]);
    }
}
