using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fantasma : MonoBehaviour
{
    [Header("Components")]
    private Animator _Animator;
    private Rigidbody2D Rig;
    private AudioController _AudioController;
    private AudioSource _AudioSource;
    private SpriteRenderer SR;
    private BoxCollider2D _BC;

    [SerializeField]
    private Fantasma_Trigger _Fantasma_Trigger;

    [Header("Attack variables")]
    private Player _Player;
    public float speed = 6f;

    [Header("Killed")]
    [SerializeField]
    private float Kill_area = 8f;
    public AudioClip Death_sound;

    private void Start()
    {
        _AudioSource = this.GetComponent<AudioSource>();
        _AudioController = FindObjectOfType(typeof(AudioController)) as AudioController;
        _Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Rig = this.GetComponent<Rigidbody2D>();
        _Animator = this.GetComponent<Animator>();
        _Fantasma_Trigger = GetComponentInChildren<Fantasma_Trigger>();
        _AudioSource.volume = 0f;

        SR = this.GetComponent<SpriteRenderer>();
        _BC = this.GetComponent<BoxCollider2D>();

        SR.enabled = false;
        _BC.enabled = false;
    }

    private void Update()
    {
        if(_Fantasma_Trigger.CanAttack == true)
        {
            SR.enabled = true;
            _BC.enabled = true;

            _AudioSource.volume = 1f;
            //Do the animation and attack
            transform.position = Vector2.MoveTowards(transform.position, _Player.transform.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, _Player.transform.position) < Kill_area && _Player.LightOn == true)
            {
                _AudioController.AudioPlay(Death_sound, 1f);
                this.gameObject.SetActive(false);
                Destroy(this.gameObject, 1f);
            }

            if(Vector2.Distance(transform.position, _Player.transform.position) < 0.1f)
            {
                _AudioSource.volume = 0f;
            }


        }
    }


}