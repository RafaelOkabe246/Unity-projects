using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    public AudioSource Music;

    public AudioClip battlemusc;
    public AudioClip NarrativeMusic;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(AudioClip música, float volume)
    {
        Music.PlayOneShot(música, volume);
    }

     void Update()
    {

    }

}
