using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    [Header("Emissor da a��o")]
    public Character Emissor;

    [Header("Individual target")]
    public Character Target;

    [Header("Multiple target")]
    public List<Character> Targets;

}

