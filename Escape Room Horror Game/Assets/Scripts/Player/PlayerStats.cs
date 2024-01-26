using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Player atributtes")]
public class PlayerStats : ScriptableObject
{
    public bool isAlive;
    public int ammunitionNumber;

    private void OnEnable()
    {
        isAlive = true;
        ammunitionNumber = 6;
    }
}
