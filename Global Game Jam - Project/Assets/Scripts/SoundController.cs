using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] internal AudioSource SFXsounds;
    [SerializeField] internal AudioSource Musics;

    public float music_volume = 0.2f;

    [Header("Audio clips")]
    public AudioClip[] Steps;

    public AudioClip Music;

    [SerializeField]
    private int Select_music;



    private void Awake()
    {
        SFXsounds.gameObject.SetActive(true);
        Musics.gameObject.SetActive(true);
    }

    void Start()
    {
        Musics.clip = Music;
        Musics.volume = music_volume;
        Musics.Play();
    }

    public void AudioPlay(AudioClip sfxClip, float volume)
    {
        SFXsounds.PlayOneShot(sfxClip, volume);
    }


    public void Mute_Allsounds()
    {
        Musics.gameObject.SetActive(false);
        SFXsounds.gameObject.SetActive(false);
    }

    public void Change_music()
    {
        SFXsounds.gameObject.SetActive(true);
        Musics.gameObject.SetActive(true);
        Musics.clip = Music;
        Musics.volume = music_volume;
        Musics.Play();

    }

    public void Mute_music()
    {
        Musics.Stop();
    }
}
