using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostEnemy : basicEnemy
{
    //public GameObject falseText;
    //The ghost enemy randomize the letters during 3 seconds

    /*[SerializeField] float randomTimeFinish = 6f;
    [SerializeField] float randomTimeStart = 3f;
    [SerializeField] float randomCount;*/

    private void Start()
    {
        //target = player.transform;
        //falseText.SetActive(false);
    }

    private void Update()
    {
        /*randomCount += Time.deltaTime;
        if (randomCount > randomTimeStart)
        {
            HideLetters();        
        }
        else if (randomCount > randomTimeFinish)
        {
            //Turn the text back to normal
            ShowLetters();

        }*/

    }

    /*void ShowLetters()
    {
        falseText.SetActive(false);
        randomCount = 0;
    }

    void HideLetters()
    {
        falseText.SetActive(true);
       
    }*/
}
