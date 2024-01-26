using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Física e movimentação")]
    public float speed = 0.5f;
    [SerializeField] private Rigidbody2D Rig;
    public Vector2 movement;
    public bool isMoving;
    [SerializeField] private Animator _Animator;
    [SerializeField] private bool isInvencible;

    [Header("Luzes")]
    public bool LightOn;
    public Light _Light;
    private float LowLight = 0.2f;
    private float HighLight = 1.0f;
    private Batery_bar _Batery;

    [Header("Inimigos")]
    //public bool Foi_pego;
    public bool Dead;


    [Header("Respawn and scenes")]
    public static int CenaAtual;
    [SerializeField] private Checkpoint_manager _Checkpoint_manager;
    public int lifes;

    [Header("Interactables")]
    public int GoldenKeys;
    public int SilverKeys;
    public KeyCode Interact;
    private Door _Door;
    public Paper_Narrative Paper;
    public UnityEvent Faca;

    [Header("UI")]
    public Camera MainCamera;
    public RawImage bloodEfect;
    Color bloodColor;

    [Header("Audio")]
    private AudioController _AudioController;

    private void Awake()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _Checkpoint_manager = GameObject.FindGameObjectWithTag("CM").GetComponent<Checkpoint_manager>();
    }

    void Start()
    {
        bloodColor = bloodEfect.color;
        lifes = 3;
        if (_Checkpoint_manager.Has_Checkpoint == false)
        {
            transform.position = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Transform>().position;
        }
        else if (_Checkpoint_manager.Has_Checkpoint == true && _Checkpoint_manager.LastCheckpointPos != new Vector2(0,0))
        {
            transform.position = new Vector2(_Checkpoint_manager.LastCheckpointPos.x, _Checkpoint_manager.LastCheckpointPos.y);
        }

        //Components
        Rig = this.GetComponent<Rigidbody2D>();
        _Animator = this.GetComponent<Animator>();
        _AudioController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioController>();
        _Door = FindObjectOfType(typeof(Door)) as Door;
        _Batery = FindObjectOfType(typeof(Batery_bar)) as Batery_bar;

        //Respawn
        CenaAtual = SceneManager.GetActiveScene().buildIndex;

        GoldenKeys = 0;
        SilverKeys = 0;

    }

    void FixedUpdate()
    {
        moveCharacter(movement);
    }

    private void OnGUI()
    {
        if (Input.GetKey(KeyCode.P))
        {
            //Active pause
        }
    }

    void Update()
    {
        if(lifes == 0)
        {
            Dead = true;
            _Light.gameObject.SetActive(false);
            Execute_Jumpscare();
        }


        if (Input.GetKeyDown(KeyCode.Space) && LightOn == false && Batery_bar.Can_Light == true)
        {
            Batery_bar.instance.UseLantern(10);
            LightOn = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && LightOn == true)
        {
            Batery_bar.Using_battery = false;
            LightOn = false;
        }
        if(Batery_bar.Can_Light == false)
        {
            LightOn = false;
        }

        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        LightController();

        //animation variables
        _Animator.SetBool("isMoving", isMoving);

    }

    void moveCharacter(Vector2 direction)
    {
        Rig.velocity = direction * speed;
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (x != 0 || y != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if (x != 0 && y != 0)
        {
            speed = 2;
        }
        else
        {
            speed = 3;
        }

    }
    void OnTriggerEnter2D(Collider2D collision)
    {

         if (collision.gameObject.CompareTag("GoldenKey"))
        {
            GoldenKeys ++;
            Destroy(collision.gameObject);
            _AudioController.AudioPlay(_AudioController.Key, 1f);
        }
        else if (collision.gameObject.CompareTag("Luz"))
        {
            _Batery.currentBatery = _Batery.maxBattery;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Door") && _Door.isClose == false)
        {
            _Animator.SetTrigger("Door");
        }
        else if (collision.gameObject.CompareTag("Paper"))
        {
            Paper = collision.gameObject.GetComponent<Paper_Narrative>();
            Paper.Texto();
        }
         /*
        else if (collision.gameObject.CompareTag("Fantasma"))
        {
            _Light.gameObject.SetActive(false);
            Foi_pego = true;
            Execute_Jumpscare();
            Dead = true;
        }
        else if (collision.gameObject.CompareTag("Dark monster"))
        {
            _Light.gameObject.SetActive(false);
            Foi_pego = true;
            Execute_Jumpscare();
            Dead = true;
        }
         */
         
        else if (collision.gameObject.layer == 10)
        {
            TakeDamage();
        }
         
        else if (collision.gameObject.CompareTag("knife"))
        {
            Faca.Invoke();
        }

    }

    IEnumerator Terminar(float waitTime)
    {
        _AudioController.AudioPlay(_AudioController.Knife, 1f);
        yield return new WaitForSeconds(waitTime);
        Application.Quit();
    }

    public void Fim()
    {
        _Light.gameObject.SetActive(false);
        StartCoroutine(Terminar(0.8f));
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isInvencible)
        {
            if (collision.gameObject.CompareTag("Hunter"))
            {
               // _Light.gameObject.SetActive(false);
                //Dead = true;
                TakeDamage();
            }
            else if (collision.gameObject.CompareTag("Blind demon"))
            {
                //_Light.gameObject.SetActive(false);
                //Dead = true;
                TakeDamage();
            }
            else if (collision.gameObject.CompareTag("Dark monster"))
            {
                //_Light.gameObject.SetActive(false);
                //Dead = true;
                TakeDamage();
            }
        }
    }


    void TakeDamage()
    {
        if (!Dead)
        {
            lifes -= 1;
            StartCoroutine(GetInvunerable());
        }
    }

    IEnumerator GetInvunerable()
    {
        isInvencible = true;
        Physics2D.IgnoreLayerCollision(8, 10, true);
        //Set a time for the blood color appears
        bloodColor.a = bloodColor.a + 0.3f;
        bloodEfect.color = bloodColor;

        yield return new WaitForSeconds(1f);
        
        Physics2D.IgnoreLayerCollision(8, 10, false);
        isInvencible = false;
    }

    /*
    IEnumerator CameraShake(float duration, float magnitude)
    {
        Vector3 originalPos = MainCamera.transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            MainCamera.transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            //Wait until the next frame
            yield return null;
        }

        MainCamera.transform.localPosition = originalPos;
    }
    */
    void LightController()
    {
        if(LightOn == true)
        {
            _Light.intensity = HighLight;
            _Light.range = 30f;
        }
        else
        {
            _Light.intensity = LowLight;
            _Light.range = 15f;
        }
    }

    void Execute_Jumpscare()
    {
        _Batery.gameObject.SetActive(false);

        _AudioController.AudioPlay(_AudioController.Dead, 0.8f);
        

        StartCoroutine("Jumpscare");
    }

    IEnumerator Jumpscare()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Dead screen");
    }
    
    public void Next_scene()
    {
        lifes = 3;
        _Checkpoint_manager.LastCheckpointPos = new Vector2(0, 0);
        SceneManager.LoadScene(CenaAtual + 1);
        _AudioController.Change_music();
    }
    
}
