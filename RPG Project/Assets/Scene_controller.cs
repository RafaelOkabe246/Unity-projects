using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene_controller : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector End_level;


    [Header("Scene transitions")]

    public int Next_scene;
    private int Actual_scene;
    
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

        End_level = GameObject.FindGameObjectWithTag("End level").GetComponent<PlayableDirector>();
        Actual_scene = SceneManager.GetActiveScene().buildIndex;
        Next_scene = SceneManager.GetActiveScene().buildIndex + 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Phase_ending()
    {
        End_level.Play();
    }

    public void Next_level()
    {
        SceneManager.LoadScene(Next_scene);
    }
}
