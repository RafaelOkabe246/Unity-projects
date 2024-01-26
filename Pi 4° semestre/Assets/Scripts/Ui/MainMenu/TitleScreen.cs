using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    public GameObject[] gameObjectsToActivate;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.anyKeyDown) 
        {
            InitializeSaveSystem();

            StartCoroutine(CloseTitleScreen());
        }
    }

    void InitializeSaveSystem()
    {
        //SaveSystem.GenerateSaveSlots(1);
    }

    private IEnumerator CloseTitleScreen() 
    {
        anim.SetTrigger("Close");

        yield return new WaitForSeconds(0.5f);

        foreach (GameObject go in gameObjectsToActivate)
        {
            go.SetActive(true);
        }

        gameObject.SetActive(false);
    }
}
