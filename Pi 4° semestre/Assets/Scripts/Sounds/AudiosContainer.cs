using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio Container", menuName = "Audios Container")]
public class AudiosContainer : ScriptableObject
{
    public AudioEvent[] audios;
}
