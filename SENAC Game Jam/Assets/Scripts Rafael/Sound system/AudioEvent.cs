using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class AudioEvent
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
}
