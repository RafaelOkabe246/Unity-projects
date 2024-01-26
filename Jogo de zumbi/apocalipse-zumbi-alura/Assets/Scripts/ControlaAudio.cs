using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaAudio : MonoBehaviour
{
    [SerializeField] 
    private AudioSource meuAudioSource;
    public static AudioSource instancia;

    // Start is called before the first frame update
    void Awake()
    {
        meuAudioSource = GetComponent<AudioSource>();
        instancia = meuAudioSource;
    }
}
