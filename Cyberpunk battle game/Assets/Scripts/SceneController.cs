using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneState { Battle, Narrative }

public class SceneController : MonoBehaviour
{
    private static SceneController instance;

    public SceneState State;

    public MusicManager Música;

    public string BattleScene;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Música = GetComponent<MusicManager>();
    }

    private void Update()
    {

    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(BattleScene);
    }

}
