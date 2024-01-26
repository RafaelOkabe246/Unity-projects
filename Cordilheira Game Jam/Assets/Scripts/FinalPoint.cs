using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FinalPoint : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Sprite aberto;
    public Sprite fechado;
    public GameObject key;
    private SceneController _SceneController;
    private void Start()
    {
        _SceneController = FindObjectOfType(typeof(SceneController)) as SceneController;
    }

    private void Update()
    {
        if(key == null)
        {
            sprite.sprite = aberto;
        }
        else
        {
            sprite.sprite = fechado;
        }
    }

    public void NextPhase()
    {
        if (key == null)
        {
            if (SceneManager.GetActiveScene().name == "Level 11")
            {
                _SceneController.Menu();
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

}
