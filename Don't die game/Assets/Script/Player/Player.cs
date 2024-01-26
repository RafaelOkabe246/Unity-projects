using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int lifes;
    
    public GameObject Particle;

    [Header("Movement variables")]
    public float moveSpeed = 400f;
    public float movement = 0f;
    private float ScreenWidth;

    private GameController _GameController;

    [Header("Color variables")]
    private SpriteRenderer PlayerRS;
    float AlphaLevel;
    public Color Hit;
    private bool isHit;

    void Start()
    {
        lifes = 3;
        PlayerRS = GetComponent<SpriteRenderer>();

        ScreenWidth = Screen.width;
    }

    void Update()
    {
        Hit = new Color(1f, 1f, 1f, AlphaLevel);
        //Mobile_movement();
        movement = Input.GetAxisRaw("Horizontal");
        CheckLife();

        if (isHit == true)
        {
            PlayerRS.color = Hit;
        }
    }

    private void FixedUpdate()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, movement * Time.fixedDeltaTime * -moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isHit = true;

        if (lifes <= 0) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else 
        {
            Instantiate(Particle, transform.position, Quaternion.identity);
            lifes -= 1;
        }

    }

    void CheckLife()
    {
        if(lifes == 2)
        {
            AlphaLevel = 0.75f;
        }
        else if(lifes == 1)
        {
            AlphaLevel = 0.5f;
        }
        else if(lifes == 0)
        {
            AlphaLevel = 0.25f;
        }
    }


    void Mobile_movement()
    {
        int i = 0;
        while (i < Input.touchCount)
        {
            if (Input.GetTouch(i).position.x > ScreenWidth / 2)
            {
                movement = 1.0f;
            }
            if (Input.GetTouch(i).position.x < ScreenWidth / 2)
            {
                movement = -1.0f;
            }
            ++i;
        }
    }

}
