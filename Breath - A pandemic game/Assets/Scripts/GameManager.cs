using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    public IEnumerator Start()
    {
        yield return new WaitForSeconds(34);
        //Play background animation

        //Play cards animation
    }
}
