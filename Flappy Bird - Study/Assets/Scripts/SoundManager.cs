using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource sfxSource;

    public AudioClip flapClip;
    public AudioClip dieClip;
    public AudioClip pointClip;

    private void Awake()
    {
        instance = this;
    }

    public void PlaySound(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip, 0.2f);
    }
}
