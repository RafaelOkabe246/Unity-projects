using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Narration line", menuName = "Dialogue")]
public class NarrationLine : ScriptableObject
{
    public NarrationText[] sentences;
}
[System.Serializable]
public struct NarrationText
{
    public string name;
    public string[] texts;
}
