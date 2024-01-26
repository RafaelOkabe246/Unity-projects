using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum InteractionType
{
    Fire,
    Water,
    Slice,
    Pass,
    Wind
}

public class Identifier : MonoBehaviour
{
    public InteractionType interactionType;
}
