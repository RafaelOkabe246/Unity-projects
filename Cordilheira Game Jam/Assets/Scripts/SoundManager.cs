using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource SFXsounds;
    public AudioSource Musics;

    public float music_volume = 0.4f;

    [Header("Audio clips")]
    public AudioClip[] Music;
    public AudioClip PlayerSteps;
    public AudioClip CompleteLevel;
    public AudioClip DrawEffect;

    [SerializeField]
    private int Select_music;

    void Start()
    {
        SFXsounds.mute = false;
        Musics.mute = false;

        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);


        Select_music = Random.Range(0, Music.Length);
        Musics.clip = Music[Select_music];
        Musics.volume = music_volume;
        Musics.Play();

    }

    public void AudioPlay(AudioClip sfxClip, float volume)
    {
        SFXsounds.PlayOneShot(sfxClip, volume);
        
    }


    public void Mute_Allsounds()
    {
        Musics.mute = true;
        SFXsounds.mute = true;
    }

    public void Change_music()
    {
        SFXsounds.mute = false;
        Musics.mute = false;
        Select_music = Random.Range(0, 5);
        Musics.clip = Music[Select_music];
        Musics.volume = music_volume;
        Musics.Play();
    }

    public void Mute_music()
    {
        Musics.Stop();
    }
}
