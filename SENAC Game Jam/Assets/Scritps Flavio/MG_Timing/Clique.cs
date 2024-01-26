using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clique : MonoBehaviour {
    private Animator anim;
    public bool hasClicked;
    void Start()
    {
        hasClicked = false;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !hasClicked)
        {
            hasClicked = true;
            anim.SetTrigger("Defendeu");
        }
    }
}
