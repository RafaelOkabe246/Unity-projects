using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mobile_movement : MonoBehaviour
{
    public int lifes;

    public GameObject Particle;

    private Rigidbody2D characterBody;

    [Header("Movement variables")]
    public float moveSpeed = 600f;
    private float ScreenWidth;
    float movement = 0f;

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

        characterBody = GetComponent<Rigidbody2D>();

        ScreenWidth = Screen.width;
    }

    void Update()
    {
        Hit = new Color(1f, 1f, 1f, AlphaLevel);
        int i = 0;
        while (i < Input.touchCount)
        {
            if (Input.GetTouch(i).position.x > ScreenWidth / 2)
            {
                RunCharacter(1.0f);
            }
            if (Input.GetTouch(i).position.x > ScreenWidth / 2)
            {
                RunCharacter(-1.0f);
            }
            ++i;
        }

        CheckLife();

        if (isHit == true)
        {
            PlayerRS.color = Hit;
        }
    }

    private void FixedUpdate()
    {

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
        if (lifes == 2)
        {
            AlphaLevel = 0.75f;
        }
        else if (lifes == 1)
        {
            AlphaLevel = 0.5f;
        }
        else if (lifes == 0)
        {
            AlphaLevel = 0.25f;
        }
    }

    private void RunCharacter(float horizontalinput)
    {
        //move player
        //characterBody.AddForce(new Vector2(horizontalinput * moveSpeed * Time.deltaTime, 0));
        transform.RotateAround(Vector3.zero, Vector3.forward, horizontalinput * Time.fixedDeltaTime * -moveSpeed);

    }

    void Moblie_movement()
    {
        int i = 0;
        while (i < Input.touchCount)
        {
            if (Input.GetTouch(i).position.x > ScreenWidth / 2)
            {
                RunCharacter(1.0f);
            }
            if (Input.GetTouch(i).position.x < ScreenWidth / 2)
            {
                RunCharacter(-1.0f);
            }
            ++i;
        }
    }
}
