using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    public GameObject[] SceneDialogues;

    public GameObject Actual_dialogBox;
    
    private PlayerManager Player;

    private Dialog DialogBox;


    private void Start()
    {
        Player = FindObjectOfType(typeof(PlayerManager)) as PlayerManager;
        DialogBox = FindObjectOfType(typeof(Dialog)) as Dialog;

    }

    void Update()
    {
        if (Player.Player_is_on_dialogBox == true)
        {
           Actual_dialogBox = Player.DialogBox;
        }
        else
        {
            Actual_dialogBox = null;
        }
    }

    public void Contacting_the_DialogueBox()
    {
        Actual_dialogBox.GetComponent<Dialog>().nextSentence();
    }
}
