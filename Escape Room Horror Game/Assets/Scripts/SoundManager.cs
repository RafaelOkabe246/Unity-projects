using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioClip[] musics; 


    public AudioSource _sfxSource;
    public AudioSource _musicSource;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(PlayMusic());
    }
    IEnumerator PlayMusic()
    {
        _musicSource.clip = musics[Random.Range(0, musics.Length - 1)];
        _musicSource.Play();
        yield return new WaitForSeconds(_musicSource.clip.length);
        StartCoroutine(PlayMusic());
    }

    public void PlaySFX(AudioClip audioClip)
    {
        _sfxSource.PlayOneShot(audioClip);
    }
}
