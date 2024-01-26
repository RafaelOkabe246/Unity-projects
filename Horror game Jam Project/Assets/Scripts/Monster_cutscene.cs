using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Monster_cutscene : MonoBehaviour
{
    [SerializeField]
    private Checkpoint_manager _Checkpoint_manager;

    private void Start()
    {
        _Checkpoint_manager = FindObjectOfType(typeof(Checkpoint_manager)) as Checkpoint_manager;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Final");
            _Checkpoint_manager.LastCheckpointPos = new Vector2(0, 0);
        }
    }

}
