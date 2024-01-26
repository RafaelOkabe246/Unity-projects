using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JunpScarer_Controller : MonoBehaviour
{
    private Player _Player;
    private AudioController _AudioController;


    private Camera _camera;
    public Image Hunter;
    public Image Blind_demon;
    private GameObject Green_bar;

    void Start()
    {
        _AudioController = FindObjectOfType(typeof(AudioController)) as AudioController;
        _Player = FindObjectOfType(typeof(Player)) as Player;
        _camera = FindObjectOfType(typeof(Camera)) as Camera;
        Green_bar = GameObject.FindGameObjectWithTag("Battery bar");
    }

    public void Check_and_jumpscare()
    {
        Debug.Log("funcionou");
           
        Green_bar.SetActive(false);
        _AudioController.Mute_Allsounds();
        _AudioController.AudioPlay(_AudioController.Dead, 1f);
        
        StartCoroutine("Jumpscare");
    }

    IEnumerator Jumpscare()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Dead screen");
    }

}
