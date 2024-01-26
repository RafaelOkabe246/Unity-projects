using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Objects_color
{
    White,
    Black
}
public class ObjectsManager : MonoBehaviour
{
    public List<GameObject> W_objects = new List<GameObject>();
    public List<GameObject> B_objects = new List<GameObject>();

    public Player _Player;

    public Objects_color Current_color;

    void Start()
    {
        AddObjects(W_objects, Tags.White_object);
        AddObjects(B_objects, Tags.Black_object);

        _Player = FindObjectOfType(typeof(Player)) as Player;
    }
    private void Update()
    {
        ColorManager();
    }

    public void ChangeColorBlack()
    {
        Current_color = Objects_color.Black;
    }

    public void ChangeColorWhite()
    {
        Current_color = Objects_color.White;
    }

    public void ColorManager()
    {
        if (Current_color == Objects_color.Black)
        {
            foreach (GameObject gameObject in W_objects)
            {
                gameObject.SetActive(false);
            }

            foreach (GameObject gameObject in B_objects)
            {
                gameObject.SetActive(true);
            }
        }
        else if (Current_color == Objects_color.White)
        {
            foreach (GameObject gameObject in W_objects)
            {

                if (gameObject == null)
                {
                    break;
                }
                else
                {
                    gameObject.SetActive(true);
                }
            }

            foreach (GameObject gameObject in B_objects)
            {

                if (gameObject == null)
                {
                    break;
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }

    void AddObjects(List<GameObject> objects, string tag)
    {
        foreach (GameObject _object in GameObject.FindGameObjectsWithTag(tag))
        {
            if (_object == null)
            {
                break;
            }
            else
            {
                objects.Add(_object);
            }
        }
    }
}
