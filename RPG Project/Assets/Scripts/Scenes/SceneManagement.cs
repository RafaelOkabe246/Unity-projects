using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    private DoorsController _DoorsController;
    private int next_scene_to_load;

    void Start()
    {
        _DoorsController = FindObjectOfType(typeof(DoorsController)) as DoorsController;
        next_scene_to_load = SceneManager.GetActiveScene().buildIndex + 1;
    }


    public void Open_the_next_scene()
    {
        if (_DoorsController.isOpen == true)
        {
            SceneManager.LoadScene(next_scene_to_load);
        }
    }
}
