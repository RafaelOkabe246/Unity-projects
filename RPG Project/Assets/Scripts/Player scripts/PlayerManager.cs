using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool TasksComplete;
    public int tasks;
    
    public bool Player_is_on_dialogBox;
    public GameObject DialogBox;

    public GameObject Thecharacter;

    private void Start()
    {
        tasks = 0;
    }
    void Update()
    {
        if (tasks == 1)
        {
            TasksComplete = true;
        }
        if(Input.GetKeyDown(KeyCode.E) == true && DialogueManager.Is_talking == true)
        {
            Thecharacter.GetComponent<Girl>().Dialog_Cutscene_Stop();
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DialogueManager"))
        {
            DialogBox = collision.gameObject;
            Player_is_on_dialogBox = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DialogueManager"))
        {
            DialogBox = null;
            Player_is_on_dialogBox = false;
        }
    }
    public void Made_a_task()
    {
        tasks ++;
    }

    public void Tasks_completed()
    {
        TasksComplete = true;
    }
}
