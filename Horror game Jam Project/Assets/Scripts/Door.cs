using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private Animator _Animator;
    public bool isClose;
    private Player _Player;

    [SerializeField]
    private Checkpoint_manager _Checkpoint_manager;

    void Start()
    {
        _Checkpoint_manager = FindObjectOfType(typeof(Checkpoint_manager)) as Checkpoint_manager;
        _Animator = GetComponent<Animator>();
        _Player = FindObjectOfType(typeof(Player)) as Player;
        isClose = true;
    }

    void Update()
    {
        _Animator.SetBool("isClose", isClose);

        if(_Player.GoldenKeys == 1)
        {
            isClose = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            string activeScene = SceneManager.GetActiveScene().name;
            PlayerPrefs.SetString("LevelSaved", activeScene);

            Debug.Log(activeScene);

            gameObject.SetActive(false);
        }
    }
}
