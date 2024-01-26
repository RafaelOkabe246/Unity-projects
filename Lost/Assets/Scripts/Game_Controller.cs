using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Controller : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource sfxSource;
    public AudioSource musicSource;

    public Transform PlayerTransform;

    public GameObject Player;

    public Transform LimiteCamEsq, LimiteCamDir, LimiteCamTop, LimiteCamDown;
    public float SpeedCam;
    public Camera Cam;

    [Header("Begining phase points")]
    public int Phases;


    public Transform LimiteCamEsq_2, LimiteCamDir_2, LimiteCamTop_2, LimiteCamDown_2;

    public Transform LimiteCamEsq_Boss, LimiteCamDir_Boss, LimiteCamTop_Boss, LimiteCamDown_Boss;




    void Start()
    {
        Phases = 1;
        Cam = Camera.main;
    }

    void Update()
    {

    }

    void LateUpdate()
    {
        CamController();
    }

    void CamController()
    {
        if (Phases == 1)
        {
            Scene1view();
        }
        else if (Phases == 2)
        {
            Scene2view();
        }
        else if (Phases == 3)
        {
            CamBossView();
        }
    }

    void Scene1view()
    {
        float cam_positionx = PlayerTransform.position.x;
        float cam_positiony = PlayerTransform.position.y;

        //Scene 1
        if (Cam.transform.position.x < LimiteCamEsq.position.x && PlayerTransform.position.x < LimiteCamEsq.position.x)
        {
            cam_positionx = LimiteCamEsq.position.x;
        }
        else if (Cam.transform.position.x > LimiteCamDir.position.x && PlayerTransform.position.x > LimiteCamDir.position.x)
        {
            cam_positionx = LimiteCamDir.position.x;
        }
        if (Cam.transform.position.y < LimiteCamDown.position.y && PlayerTransform.position.y < LimiteCamDown.position.y)
        {
            cam_positiony = LimiteCamDown.position.y;
        }
        else if (Cam.transform.position.y > LimiteCamTop.position.y && PlayerTransform.position.y > LimiteCamTop.position.y)
        {
            cam_positiony = LimiteCamDown.position.y;
        }

        Vector3 cam_position = new Vector3(cam_positionx, cam_positiony, Cam.transform.position.z);
        Cam.transform.position = Vector3.Lerp(Cam.transform.position, cam_position, SpeedCam * Time.deltaTime);
    }

    void Scene2view()
    {
        float cam_positionx = PlayerTransform.position.x;
        float cam_positiony = PlayerTransform.position.y;

        //Scene 2
        if (Cam.transform.position.x < LimiteCamEsq_2.position.x && PlayerTransform.position.x < LimiteCamEsq_2.position.x)
        {
            cam_positionx = LimiteCamEsq_2.position.x;
        }
        else if (Cam.transform.position.x > LimiteCamDir_2.position.x && PlayerTransform.position.x > LimiteCamDir_2.position.x)
        {
            cam_positionx = LimiteCamDir_2.position.x;
        }
        if (Cam.transform.position.y < LimiteCamDown_2.position.y && PlayerTransform.position.y < LimiteCamDown_2.position.y)
        {
            cam_positiony = LimiteCamDown_2.position.y;
        }
        else if (Cam.transform.position.y > LimiteCamTop_2.position.y && PlayerTransform.position.y > LimiteCamTop_2.position.y)
        {
            cam_positiony = LimiteCamTop_2.position.y;
        }

        Vector3 cam_position = new Vector3(cam_positionx, cam_positiony, Cam.transform.position.z);
        Cam.transform.position = Vector3.Lerp(Cam.transform.position, cam_position, SpeedCam * Time.deltaTime);
    }

    void CamBossView()
    {
        float cam_positionx = PlayerTransform.position.x;
        float cam_positiony = PlayerTransform.position.y;

        //Boss view
        if (Cam.transform.position.x < LimiteCamEsq_Boss.position.x && PlayerTransform.position.x < LimiteCamEsq_Boss.position.x)
        {
            cam_positionx = LimiteCamEsq_Boss.position.x;
        }
        else if (Cam.transform.position.x > LimiteCamDir_Boss.position.x && PlayerTransform.position.x > LimiteCamDir_Boss.position.x)
        {
            cam_positionx = LimiteCamDir_Boss.position.x;
        }
        if (Cam.transform.position.y < LimiteCamDown_Boss.position.y && PlayerTransform.position.y < LimiteCamDown_Boss.position.y)
        {
            cam_positiony = LimiteCamDown_Boss.position.y;
        }
        else if (Cam.transform.position.y > LimiteCamTop_Boss.position.y && PlayerTransform.position.y > LimiteCamTop_Boss.position.y)
        {
            cam_positiony = LimiteCamTop_Boss.position.y;
        }

        Vector3 cam_position = new Vector3(cam_positionx, cam_positiony, Cam.transform.position.z);
        Cam.transform.position = Vector3.Lerp(Cam.transform.position, cam_position, SpeedCam * Time.deltaTime);
    }


    public void playSFX(AudioClip sfxClip, float volume)
    {
        sfxSource.PlayOneShot(sfxClip, volume);
    }

   
}
