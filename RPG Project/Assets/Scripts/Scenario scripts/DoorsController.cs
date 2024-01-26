using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorsController : MonoBehaviour
{
    public bool isOpen;
    public UnityEvent Load_the_next_scene;

    public void OpenDoor(GameObject obj)
    {
        if (!isOpen)
        {
            PlayerManager manager = obj.GetComponent<PlayerManager>();
            if (manager)
            {
                if (manager.TasksComplete == true)
                {
                    isOpen = true;
                    Load_the_next_scene.Invoke();
                }
            }
        }
    }
}
