using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{

    private Transform trans;
    private Collider2D col;

    private bool click; //check if the player has clicked with the mouse

    public BattleSystem bs;

    void Start()
    {
        trans = GetComponent<Transform>();
        col = GetComponent<Collider2D>();
        col.enabled = false;
    }

    void Update()
    {

        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Checking the mouse click
        if (Input.GetMouseButtonDown(0) && !click)
        {
            Debug.Log("CLICK!!");
            click = true;
            col.enabled = true;
        }

        if (Input.GetMouseButtonUp(0)) {
            click = false;
            col.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Unit" && click) {
            //Select the target
            Debug.Log("TARGET SELECTED!");
            bs.selectedButton.skillTargetUnit = collision.GetComponent<Unit>();
            bs.selectedButton.SkillOnTarget();
            click = false;
        }
    }
}
