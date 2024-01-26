using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXSounds : MonoBehaviour
{
    private Player _Player;

    public AudioSource SFXSource;
    public AudioSource MusicsSource;

    public AudioClip Dead;

    public void playSFX(AudioClip sfxCLip, float volume)
    {
        SFXSource.PlayOneShot(sfxCLip,volume);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    void Death()
    {

    }
}
