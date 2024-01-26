using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blind_demon : MonoBehaviour
{
    private Player _Player;
    public LayerMask Player;
    public bool isListening;
    public float Hearing_Area;
    private Animator _Animator;

    public float retreatingDistance;
    public float Can_see_light;
    public float speed;

    private AudioSource Steps;

    void Start()
    {
        _Player = FindObjectOfType(typeof(Player)) as Player;
        _Animator = GetComponent<Animator>();
        Steps = GetComponent<AudioSource>();
        retreatingDistance = 10f;
        Hearing_Area = 5f;
        Can_see_light = 7f;
    }

    void Update()
    {
        Sound();
        Hunting();
    }

    void Hunting()
    {
        isListening = Physics2D.OverlapCircle(transform.position, Hearing_Area, Player);

        if (isListening == true && _Player.isMoving == true && _Player.LightOn == false)
        {
            //Chasing player
            if (Vector2.Distance(transform.position, _Player.transform.position) < 2f && speed > 1f)
            {
                speed -= speed * Time.deltaTime;
            }
            transform.position = Vector2.MoveTowards(transform.position, _Player.transform.position, speed * Time.deltaTime);
            _Animator.SetBool("isListening", true);
        }
        else if (isListening == false  &&  _Player.isMoving == false)
        {
            //Está parado
            speed = 3f;
            _Animator.SetBool("isListening", false);
        }
        else if (Vector2.Distance(transform.position, _Player.transform.position) < Can_see_light && _Player.LightOn == true)
        {
            //Run from player
            if (Vector2.Distance(transform.position, _Player.transform.position) > 3f)
            {
                speed -= speed * Time.deltaTime;
            }
            _Animator.SetBool("isOnLight", true);
            transform.position = Vector2.MoveTowards(transform.position, _Player.transform.position, -5f * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, _Player.transform.position) > retreatingDistance && _Player.LightOn == false || Vector2.Distance(transform.position, _Player.transform.position) > retreatingDistance) 
        {
            //Se afastou da área da luz
            speed = 3f;
            _Animator.SetBool("isOnLight", false);
            _Animator.SetBool("isListening", false);    
        }

    }

    void Sound()
    {

        if (isListening == true && _Player.isMoving == true && _Player.LightOn == false)
        {
            Steps.mute = false;
        }
        else if (_Player.isMoving == false)
        {
            Steps.mute = true;
        }

        if(_Player.Dead == true)
        {
            Steps.mute = true;
        }
    }

}
