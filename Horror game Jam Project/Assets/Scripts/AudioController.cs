using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AudioController : MonoBehaviour    
{
    public AudioSource SFXsounds;
    public AudioSource Musics;

    public float music_volume = 0.4f; 

    [Header("Audio clips")]
    public AudioClip[] Steps;
    public AudioClip Key;
    public AudioClip Dead;
    public AudioClip Paper_turn;
    public AudioClip Hearth_beat;
    public AudioClip Knife;

    public AudioClip[] Music;


    [SerializeField]
    private int Select_music;



    private void Awake()
    {
        SFXsounds.mute = false;
        Musics.mute = false;
    }

    void Start()
    {
        Select_music = Random.Range(0, 5);
        Musics.clip = Music[Select_music];
        Musics.volume = music_volume;
        Musics.Play();

    }

    public void AudioPlay(AudioClip sfxClip, float volume)
    {
        SFXsounds.PlayOneShot(sfxClip, volume);
    }

    public void HeartsbeatsPlay()
    {
        Musics.PlayOneShot(Hearth_beat, 1f);
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
